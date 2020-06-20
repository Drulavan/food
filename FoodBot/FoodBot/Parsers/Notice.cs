using System;
using System.Collections.Generic;
using System.Text;

namespace FoodBot.Parsers
{
    public class Notice
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Текст поста
        /// </summary>
        public string FullText { get; set; }

        /// <summary>
        /// Ссылка на пост
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Дата поста
        /// </summary>
        public DateTime? Date { get; set; }

        public Source Source { get; set; }

        public List<string> PhotosUrl { get; set; }
    }
}
