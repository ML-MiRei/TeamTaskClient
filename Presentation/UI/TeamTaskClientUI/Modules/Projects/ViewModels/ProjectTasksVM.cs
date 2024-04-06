using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.UI.Common.Base;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    internal class ProjectTasksVM : ViewModelBase
    {

        public ProjectTasksVM(IMediator mediator) { }



        private ObservableCollection<ProjectTaskModel> _projectTasksStories;
        public ObservableCollection<ProjectTaskModel> ProjectTasksStories
        {
            get { return _projectTasksStories; }
            set { _projectTasksStories = value; }
        }


        private ObservableCollection<ProjectTaskModel> _projectTasksTodo;
        public ObservableCollection<ProjectTaskModel> ProjectTasksTodo
        {
            get { return _projectTasksTodo; }
            set { _projectTasksTodo = value; }
        }



        private ObservableCollection<ProjectTaskModel> _projectTasksInProcess;
        public ObservableCollection<ProjectTaskModel> ProjectTasksInProcess
        {
            get { return _projectTasksInProcess; }
            set { _projectTasksInProcess = value; }
        }


        private ObservableCollection<ProjectTaskModel> _projectTasksTesting;
        public ObservableCollection<ProjectTaskModel> ProjectTasksTesting
        {
            get { return _projectTasksTesting; }
            set { _projectTasksTesting = value; }
        }



        private ObservableCollection<ProjectTaskModel> _projectTasksDone;
        public ObservableCollection<ProjectTaskModel> ProjectTasksDone
        {
            get { return _projectTasksDone; }
            set { _projectTasksDone = value; }
        }
    }
}
