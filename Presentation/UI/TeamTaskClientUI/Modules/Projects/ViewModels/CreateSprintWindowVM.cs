using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.UseCases.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Storages;
using TeamTaskClient.ApplicationLayer.Interfaces.Cash;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    public class CreateSprintWindowVM : ViewModelBase
    {

        public CreateSprintWindowVM(IProjectsCash projectsCash)
        {

            DateStart = DateTime.Now;
            DateEnd = DateTime.Now.AddDays(7);

            Tasks = projectsCash.BacklogTasks.Where(t => t.Status == (int)StatusProjectTaskEnum.STORIES).ToList();
        }


        public List<ProjectTaskModel> Tasks { get; set; }



        public ICommand CloseWindow { get; } = new CloseWindowCommand();
        public ICommand CreateButton { get; } = new NewSprintCommand();
        private class CloseWindowCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {

                var win = System.Windows.Application.Current.Windows.OfType<CreateSprintWindow>().ToList();
                foreach (var w in win)
                {
                    try
                    {
                        w.DialogResult = false;
                    }
                    catch { }
                }

            }
        }


        private class NewSprintCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                if (_dateEnd < _dateStart)
                {
                    ErrorWindow.Show("The start date is later than the end date");                   
                }
                else
                {
                    System.Windows.Application.Current.Windows.OfType<CreateSprintWindow>().First().DialogResult = true;
                  
                }


            }
        }

        private static DateTime _dateEnd;
        public DateTime DateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                _dateEnd = value;
                OnPropertyChanged(nameof(DateEnd));
            }
        }




        private static DateTime _dateStart;
        public DateTime DateStart
        {
            get
            {
                return _dateStart;
            }
            set
            {
                _dateStart = value;
                OnPropertyChanged(nameof(DateStart));
            }
        }




    }
}
