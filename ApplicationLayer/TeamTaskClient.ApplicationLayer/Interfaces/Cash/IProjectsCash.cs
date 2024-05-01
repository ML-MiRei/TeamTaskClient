using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Cash
{
    public interface IProjectsCash
    {
        public ObservableCollection<ProjectModel> Projects { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }
        public ProjectModel SelectedProject { get; set; }
        public ObservableCollection<SprintModel> Sprints { get; set; }
        public SprintModel SelectedSprint { get; set; }
        public ObservableCollection<ProjectTaskModel> BacklogTasks { get; set; }
        public ObservableCollection<ProjectTaskModel> Tasks { get; set; }

        public event EventHandler<ProjectModel> SelectedProjectChanged;
        public event EventHandler<SprintModel> SelectedSprintChanged;

        public event EventHandler TaskChanged;
        public event EventHandler ProjectChanged;
        public event EventHandler SprintChanged;

    }
}
