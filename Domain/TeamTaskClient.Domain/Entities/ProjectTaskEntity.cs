using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.Domain.Entities
{
    public class ProjectTaskEntity : IBaseEntity
    {
        public int ID { get; set; }
        public int IdProject { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public StatusProjectTaskEnum StatusProjectTask { get; set; }
        public IExecutorProjectTask? ExecutorProjectTask { get; set; }


    }
}
