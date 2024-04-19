using System.ComponentModel.DataAnnotations;

namespace TeamTaskClient.ApplicationLayer.DTOs
{
    public class ProjectDTO
    {
        public int? ID { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        public string? LeadTag { get; set; }
    }
}
