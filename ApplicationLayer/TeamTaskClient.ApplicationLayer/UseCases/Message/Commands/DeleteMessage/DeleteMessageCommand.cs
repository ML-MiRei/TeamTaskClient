using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.Message.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
    }
}
