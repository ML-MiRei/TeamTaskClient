using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.Models
{
    public class MessageModel
    {
        public int ID { get; set; }
        public string CreatorTag { get; set; }
        public string Text { get; set; }
        public string UserNameCreator { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
