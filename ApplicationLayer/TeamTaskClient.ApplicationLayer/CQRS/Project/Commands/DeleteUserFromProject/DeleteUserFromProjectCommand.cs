using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.DeleteUserFromProject
{
    internal class DeleteUserFromProjectCommand : IRequest
    {
        public int ProjectId { get; set; }
        public string UserTag { get; set; }
    }
}
