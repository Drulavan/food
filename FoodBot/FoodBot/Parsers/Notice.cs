﻿using System;
using System.Collections.Generic;

namespace FoodBot.Parsers
{
    /// <summary>
    /// Это класс уведомления
    /// </summary>
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
        /// Текст на картинке (если есть)
        /// </summary>
        public string ImageText { get; set; }

        /// <summary>
        /// Ссылка на пост
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Дата поста
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Источник объявления
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// Ссылки на фото
        /// </summary>
        public List<string> PhotosUrl { get; set; }
    }
}