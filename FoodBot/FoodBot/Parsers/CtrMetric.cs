﻿namespace FoodBot.Parsers
{
    /// <summary>
    /// Класс метрик
    /// </summary>
    public class CtrMetric
    {
        public string UserID { get; set; }

        public string PostId { get; set; }

        public Source Source { get; set; }
    }
}