using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class TeamEntity
    {
        public int? ID { get; set; }
        public string? Tag { get; set; }
        public string? Name { get; set; }
        public string? TeamLeadTag { get; set; }
        public int? TeamLeadId { get; set; }

    }
}
