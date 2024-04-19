using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.ChangeDateEndSprint
{
    public class ChangeDateEndSprintHandler(ISprintRepositoryInterface sprintInterface) : IRequestHandler<ChangeDateEndSprintCommand>
    {
        public async Task Handle(ChangeDateEndSprintCommand request, CancellationToken cancellationToken)
        {
            try
            {
                sprintInterface.ChangeDateEndSprint(request.SprintId, request.DateEnd);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
