using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class MessageEntity : IBaseEntity
    {
        public int ID { get; set; }
        public int CreatorID { get; set; }
        public int ChatId { get; set; }
        public string TextMessage { get; set; }
        public string UserNameCreator { get; set; }
        public string CreatorTag { get; set; }


    }
}
