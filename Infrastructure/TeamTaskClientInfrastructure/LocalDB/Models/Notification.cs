using System.ComponentModel.DataAnnotations;

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
