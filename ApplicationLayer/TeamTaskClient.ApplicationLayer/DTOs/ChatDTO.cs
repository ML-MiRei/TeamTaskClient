using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.ApplicationLayer.DTOs
{
    public class ChatDTO
    {
        public int? ID {  get; set; }
        public string Name { get; set; }
        public int? AdminId { get; set; }
        public int ChatType { get; set; }

    }
}
