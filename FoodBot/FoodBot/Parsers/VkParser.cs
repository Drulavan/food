using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.Attachments;

namespace FoodBot.Parsers
{
    /// <summary>
    /// Этот класс занимается парсингом стен и постов ВК
    /// </summary>
    public class VkParser
    {
        private readonly VkApi api;
        private readonly int[] groups;
        private readonly NoticeRepository noticeRepository;

        public VkParser(IConfiguration configuration, NoticeRepository noticeRepository)
        {
            this.noticeRepository = noticeRepository;
            api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ClientSecret = configuration["VkClientSecret"],
                AccessToken = configuration["VkAccessToken"],
                ResponseType = ResponseType.Token,
                ApplicationId = ulong.Parse(configuration["VkAppId"]),
            });

            groups = configuration.GetSection("VkGroups").Get<int[]>();
        }

        /// <summary>
        /// Загружаем посты из ВК
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Notice>> GetNotices()
        {
            var result = new List<Notice>();

            foreach (long groupId in groups)
            {
                VkNet.Model.RequestParams.WallGetParams getParams = new VkNet.Model.RequestParams.WallGetParams()
                {
                    OwnerId = groupId,
                    Count = 10,
                };

                var wallGetObjects = await api.Wall.GetAsync(getParams);
                foreach (Post item in wallGetObjects.WallPosts)
                {
                    if (IsNew(item))
                    {
                        List<string> photos = new List<string>();
                        foreach (var atch in item.Attachments)
                        {
                            if (atch.Type.Name == "Photo")
                                photos.Add(GetPhotoUrl($"{groupId}_{atch.Instance.Id}"));
                        }

                        result.Add(new Notice()
                        {
                            Id = (long)item.Id,
                            FullText = item.Text,
                            Date = item.Date,
                            Source = 0,
                            Url = $"https://vk.com/wall{groupId}_{item.Id}",
                            PhotosUrl = photos,
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// забираем прямую ссылку на фото
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetPhotoUrl(string id)
        {
            Photo photo;
            try
            {
                photo = api.Photo.GetById(new List<string> { id }).FirstOrDefault();
            }
            catch
            {
                return "";
            }
            return photo.Sizes.OrderByDescending(x => x.Height).FirstOrDefault().Url.OriginalString;
        }

        private bool IsNew(Post post)
        {
            // return true;
            if (post.Id.HasValue)
            {
                return !noticeRepository.Exists(post.Id.Value);
            }
            return false;
        }
    }
}