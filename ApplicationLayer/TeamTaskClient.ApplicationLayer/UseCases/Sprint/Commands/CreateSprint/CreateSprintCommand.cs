using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.CreateSprint
{
    public class CreateSprintCommand : IRequest
    {
        public int ProjectId {  get; set; }
        public SprintModel SprintModel { get; set; }
    }
}
