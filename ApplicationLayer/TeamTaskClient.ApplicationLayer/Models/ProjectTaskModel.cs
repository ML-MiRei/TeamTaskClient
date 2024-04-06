using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.Models
{
    public class ProjectTaskModel
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string Detail { get; set; }
        public int Status { get; set; }
        public string Executor {  get; set; }

    }
}
