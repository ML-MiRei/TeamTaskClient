namespace TeamTaskClient.ApplicationLayer.Models
{
    public class MessageModel
    {
        public string CreatorTag { get; set; }
        public int MessageId { get; set; }
        public string TextMessage { get; set; }
        public string UserNameCreator { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
