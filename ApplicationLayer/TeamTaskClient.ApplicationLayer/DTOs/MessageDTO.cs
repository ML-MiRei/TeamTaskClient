﻿namespace TeamTaskClient.ApplicationLayer.DTOs
{
    public class MessageDTO
    {
        public int? ID { get; set; }
        public int ChatID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
    }
}
