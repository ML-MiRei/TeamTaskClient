using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.ReplyEvents
{
    public interface IProjectsEvents
    {
        public event EventHandler<ProjectModel> NewProjectAdded;
        public event EventHandler<ProjectEntity> ProjectUpdated;
        public event EventHandler<int> ProjectDeleted;
        public event EventHandler<UserModel> UserInProjectAdded;
        public event EventHandler<string> UserFromProjectDeleted;


        public event EventHandler<ProjectTaskEntity> ProjectTaskUpdated;
        public event EventHandler<ProjectTaskEntity> ProjectTaskCreated;
        public event EventHandler<int> ProjectTaskDeleted;
        public event EventHandler<ProjectTaskEntity> StatusProjectTaskChanged;
        public event EventHandler<ProjectTaskEntity> ExecuterProjectTaskChanged;


        public event EventHandler<SprintEntity> DateStartSprintUpdated;
        public event EventHandler<SprintEntity> DateEndSprintUpdated;
        public event EventHandler<SprintModel> SprintCreated;
        public event EventHandler<int> SprintDeleted;

        public event EventHandler<NotificationModel> NotificationAdded;
    }
}
