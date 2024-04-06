using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.Models
{
    public class ChatModel
    {
        public int ID { get; set; }
        public string NameChat { get; set; }
        public int Type { get; set; }
        public string Image { get; set; }
        public List<UserModel> Users { get; set; }
        public List<MessageModel> Messages { get; set; }


        public string CreatorLastMessage => Messages.FirstOrDefault(m => m.ID == Messages.Max(m => m.ID))?.UserNameCreator;
        public string LastMessage => Messages.FirstOrDefault(m => m.ID == Messages.Max(m => m.ID))?.Text;

    }
}
