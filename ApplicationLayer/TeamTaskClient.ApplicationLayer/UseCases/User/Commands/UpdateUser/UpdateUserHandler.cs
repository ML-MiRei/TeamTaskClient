using MediatR;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.User.Commands.UpdateUser
{
    public class UpdateUserHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand>
    {
        public Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                userRepository.UpdateUser(new Domain.Entities.UserEntity()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    SecondName = request.SecondName,
                    PhoneNumber = request.Phone,
                    ID = request.UserId
                });
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new UpdateException();
            }
        }
    }
}
