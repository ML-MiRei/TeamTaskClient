using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Message.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string TextMessage { get; set; }
    }
}
