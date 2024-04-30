using MediatR;

namespace TeamTaskClient.ApplicationLayer.UseCases.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public int UserId { get; set; }

    }
}
