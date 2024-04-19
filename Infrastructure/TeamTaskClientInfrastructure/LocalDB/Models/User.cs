using System.ComponentModel.DataAnnotations;

namespace TeamTaskClient.Infrastructure.LocalDB.Models
{
    internal class User
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Key]
        public string Tag { get; set; }
        public string PhoneNumber { get; set; }

        public List<Team> Teams { get; set; }
        public List<Project> Projects { get; set; }
    }
}
