using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.CreateSprint
{
    public class CreateSprintHandler(ISprintRepositoryInterface sprintInterface) : IRequestHandler<CreateSprintCommand, SprintModel>
    {
        public async Task<SprintModel> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sprint = await sprintInterface.CreateSprint(request.SprintEntity);
                return sprint;
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
