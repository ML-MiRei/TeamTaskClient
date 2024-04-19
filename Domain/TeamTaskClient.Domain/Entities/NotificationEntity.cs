using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class NotificationEntity : IBaseEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}
