using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.Models
{
    public class ProjectTaskModel
    {
        public int ProjectTaskId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }

        public string ExcutorName { get; set; }
        public bool IsUserExecutor { get; set; }

    }
}
