﻿using MediatR;

namespace TeamTaskClient.ApplicationLayer.CQRS.Project.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest
    {
        public string? ProjectName { get; set; }
        public int ProjectId { get; set; }
        public string? LeadTag { get; set; }
    }
}
