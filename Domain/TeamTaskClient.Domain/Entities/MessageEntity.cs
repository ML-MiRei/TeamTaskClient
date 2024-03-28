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

        [MaxLength(300)]
        public string Text { get; set; }
        public UserEntity Creator { get; set; }
        public DateTime DateCreated { get; set; }


    }
}
