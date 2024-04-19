using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Message.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
    }
}
