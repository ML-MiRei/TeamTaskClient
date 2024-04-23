namespace TeamTaskClient.ApplicationLayer.Models
{
    public class ChatModel
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public int UserRole { get; set; }
        public int Type { get; set; }
        public string Image { get; set; }
        public List<UserModel> Users { get; set; }
        public List<MessageModel> Messages { get; set; }

        public int ColorNumber { get; set; }


        public string Lit => ChatName[0] + "";

        public string LastMessage
        {

            get
            {
                if (Messages != null && Messages.Count != 0)
                    return Messages.FirstOrDefault(m => m.MessageId == Messages.Max(m => m.MessageId)).TextMessage;
                return "";

            }
        }

        public string CreatorLastMessage
        {

            get
            {
                if (Messages != null && Messages.Count != 0)
                    return Messages?.FirstOrDefault(m => m.MessageId == Messages.Max(m => m.MessageId)).UserNameCreator;
                return "";

            }
        }
    }
}
