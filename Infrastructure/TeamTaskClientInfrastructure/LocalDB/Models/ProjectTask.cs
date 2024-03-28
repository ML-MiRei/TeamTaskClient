using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.Infrastructure.LocalDB.Models
{
    internal class ProjectTask
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int StatusProjectTask { get; set; }
        public string? ExecutorProjectTaskTag { get; set; }
        public string? ExecutorProjectTaskType { get; set; }
        
        // 0 - User
        // 1 - Team


        public Project Project { get; set; }

        public DateTime? LastModified { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
