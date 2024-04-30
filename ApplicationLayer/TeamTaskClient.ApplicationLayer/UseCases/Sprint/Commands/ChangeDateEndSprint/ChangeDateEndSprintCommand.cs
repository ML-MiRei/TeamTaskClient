using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.ChangeDateEndSprint
{
    public class ChangeDateEndSprintCommand : IRequest
    {
        public int ProjectId {  get; set; }
        public int SprintId {  get; set; }
        public DateTime DateEnd {  get; set; }
    }
}
