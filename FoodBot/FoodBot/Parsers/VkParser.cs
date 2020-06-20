﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace FoodBot.Parsers
{
    /// <summary>
    /// Этот класс занимается парсингом стен и постов ВК
    /// </summary>
    class VkParser
    {
        VkApi api;
        int[] groups;
        public VkParser(IConfiguration configuration)
        {
            api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ClientSecret = configuration["VkClientSecret"],
                AccessToken= configuration["VkAccessToken"],
                ResponseType = ResponseType.Token,
                ApplicationId = UInt64.Parse(configuration["VkAppId"]),
            });

            groups = configuration.GetSection("VkGroups").Get<int[]>();
        }

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
                        //foreach (var atch in item.Attachments)
                        //{
                        //    if (atch.Type.Name == "Photo")
                        //        photos.Add(GetPhotoUrl(atch.Instance.ToString()));
                        //}

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

        private string GetPhotoUrl(string id)
        {
            var p = api.Photo.GetById(new List<string> { id }).FirstOrDefault();
            return p.BigPhotoSrc.AbsoluteUri.ToString();
        }

        private bool IsNew(VkNet.Model.Attachments.Post post)
        {
            return true;
        }
    }
}