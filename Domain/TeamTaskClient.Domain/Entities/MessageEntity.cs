using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class MessageEntity : IBaseEntity
    {
        public int ID { get; set; }
        public int ChatId { get; set; }
        public string TextMessage { get; set; }
        public string UserNameCreator { get; set; }
        public string CreatorTag { get; set; }


    }
}
