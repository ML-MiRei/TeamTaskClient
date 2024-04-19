using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.DeleteSprint
{
    public class DeleteSprintCommand : IRequest
    {
        public int SprintId {  get; set; }
    }
}
