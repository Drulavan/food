using System.Collections.Generic;

namespace FoodBot.Dal.Models
{
    public class UserState
    {
        public UserState(long id)
        {
            this.Id = id;
        }

        public ConversationState ConversationState { get; set; }
        public long Id { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public bool IsRegistered { get; set; }

        /// <summary>
        /// Группы ВК выбранные пользователем
        /// </summary>
        public long[] VkGroups { get; set; }

        /// <summary>
        /// Категории выбранные пользователем
        /// </summary>
        public List<Categories> Categories { get; set; }
    }
}