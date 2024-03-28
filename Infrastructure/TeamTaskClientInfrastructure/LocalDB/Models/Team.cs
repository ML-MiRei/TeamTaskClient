using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.Infrastructure.LocalDB.Models
{
    internal class Team
    {
        [Key]
        public string Tag { get; set; }
        public string Name { get; set; }

        public string LeaderTag { get; set; }

        public DateTime? LastModified { get; set; }
        public DateTime DateCreated { get; set; }

        public List<User> Users { get; set; }
        public List<Project> Projects {  get; set; }

    }
}
