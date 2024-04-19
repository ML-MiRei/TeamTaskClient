using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class ProjectEntity : IBaseEntity
    {
        public int ID { get; set; }
        public int? ProjectLeadId { get; set; }
        public string? ProjectLeadTag { get; set; }
        public string? ProjectName { get; set; }
    }
}
