using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.DeleteSprint
{
    public class DeleteSprintHandler(ISprintRepositoryInterface sprintInterface) : IRequestHandler<DeleteSprintCommand>
    {
        public async Task Handle(DeleteSprintCommand request, CancellationToken cancellationToken)
        {
            try
            {
                sprintInterface.DeleteSprint(request.SprintId);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
