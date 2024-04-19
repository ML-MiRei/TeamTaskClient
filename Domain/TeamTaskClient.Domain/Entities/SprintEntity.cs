using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class SprintEntity : IBaseEntity
    {
        public int ID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int ProjectId { get; set; }
        public int? ExecutorId { get; set; }
    }
}
