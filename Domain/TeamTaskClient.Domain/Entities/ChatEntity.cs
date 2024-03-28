using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.Domain.Entities
{
    public class ChatEntity : IBaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ChatTypeEnum ChatType { get; set; }
        public UserEntity? Admin {  get; set; } 

    }
}
