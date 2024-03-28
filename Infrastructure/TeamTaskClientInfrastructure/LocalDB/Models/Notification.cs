using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.Infrastructure.LocalDB.Models
{
    internal class Notification
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int NotificationType { get; set; }
    }
}
