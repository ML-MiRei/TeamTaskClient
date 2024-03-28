using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.DTOs
{
    public class TeamDTO
    {
        public string Name { get; set; }

        public int? TeamId { get; set; }
        public string? LeadTag { get; set; }

    }
}
