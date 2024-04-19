using MediatR;

namespace TeamTaskClient.ApplicationLayer.DTOs.Message.Command.SendMessage
{
    public class SendMessageCommand : IRequest
    {
        public int ChatId { get; set; }
        public int UderId { get; set; }
        public string TextMessage { get; set; }
    }
}
