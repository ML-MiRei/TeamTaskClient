using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.Infrastructure.LocalDB.Models
{
    internal class Chat
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? AdminTag { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Message> Messages { get; set; } 
    }
}
