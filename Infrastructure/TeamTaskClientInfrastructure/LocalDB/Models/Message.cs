﻿using System.ComponentModel.DataAnnotations;

namespace TeamTaskClient.Infrastructure.LocalDB.Models
{
    internal class Message
    {
        [Key]
        public int ID { get; set; }
        public string Text { get; set; }
        public string CreatorName { get; set; }

        public DateTime? LastModified { get; set; }
        public DateTime DateCreated { get; set; }

        public Chat Chat { get; set; }
    }
}
