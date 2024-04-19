using System.ComponentModel.DataAnnotations;

namespace TeamTaskClient.Infrastructure.LocalDB.Models
{
    internal class Project
    {
        [Key]
        public DateTime? LastModified { get; set; }
        public DateTime DateCreated { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string LeaderTag { get; set; }

        public List<ProjectTask> Tasks { get; set; }
        public List<User> Users { get; set; }
        public List<Team> Teams { get; set; }

    }
}
