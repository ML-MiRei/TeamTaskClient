using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.UI.Modules.Messanger.Models
{
    internal class MessageModel
    {
        public int Id { get; set; }
        public string TextMessage { get; set; }
        public DateTime Time {  get; set; }
        public string UserName { get; set; }
    }
}
