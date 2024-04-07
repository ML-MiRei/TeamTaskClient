using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class ProjectEntity : IBaseEntity
    {
        public int ID { get; set; }
        public int ProjectLeadId { get; set; }
        public string ProjectLeadTag { get; set; }
        public string ProjectName { get; set; }
    }
}
