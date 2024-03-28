using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.DTOs
{
    public class ProjectDTO
    {
        public int? ID {  get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        public string? LeadTag { get; set; }
    }
}
