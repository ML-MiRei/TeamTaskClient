using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.CreateSprint
{
    public class CreateSprintHandler(ISprintRepositoryInterface sprintInterface) : IRequestHandler<CreateSprintCommand>
    {
        public async Task Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            try
            {
                 await sprintInterface.CreateSprint(request.ProjectId, request.SprintModel);
               
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
