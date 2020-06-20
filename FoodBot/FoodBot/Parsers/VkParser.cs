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
    internal class VkParser
    {
        private VkApi api;
        private int[] groups;

        public VkParser(IConfiguration configuration)
        {
            api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ClientSecret = configuration["VkClientSecret"],
                AccessToken = configuration["VkAccessToken"],
                ResponseType = ResponseType.Token,
                ApplicationId = UInt64.Parse(configuration["VkAppId"]),
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
                    OwnerId = groupId
                };

                var wallGetObjects = await api.Wall.GetAsync(getParams);
                foreach (VkNet.Model.Attachments.Post item in wallGetObjects.WallPosts)
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
                            Source = Source.VK,
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
            catch (AlbumAccessDeniedException ex)
            {
                return "";
            }
            return photo.Sizes.OrderByDescending(x => x.Height).FirstOrDefault().Url.OriginalString;
        }

        private bool IsNew(VkNet.Model.Attachments.Post post)
        {
            return true;
        }
    }
}