using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.UI.Modules.Messanger.Models
{
    internal class ChatModel
    {
        public int Id { get; set; }
        public string CreatorLastMessage { get; set; }
        public string LastMessage { get; set; }
        public string Image {  get; set; }
    }
}
