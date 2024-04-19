﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Interfaces.Repositories;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Exceptions;

namespace TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.ChangeDateStartSprint
{
    public class ChangeDateStartSprintHandler(ISprintRepositoryInterface sprintInterface) : IRequestHandler<ChangeDateStartSprintCommand>
    {
        public async Task Handle(ChangeDateStartSprintCommand request, CancellationToken cancellationToken)
        {
            try
            {
                sprintInterface.ChangeDateStartSprint(request.SprintId, request.DateStart);
            }
            catch (Exception)
            {
                throw new CreateException();
            }
        }
    }
}
