using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.Sprint.Commands.CreateSprint;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;
using TeamTaskClient.Domain.Enums;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Projects.Dialogs;
using TeamTaskClient.UI.Modules.Projects.Storage;

namespace TeamTaskClient.UI.Modules.Projects.ViewModels
{
    public class SetExecutorWindowVM : ViewModelBase
    {

        public SetExecutorWindowVM()
        {

            Users = ProjectsStorage.Users;
        }


        public ObservableCollection<UserModel> Users { get; set; }



        public ICommand CloseWindow { get; } = new CloseWindowCommand();
        public ICommand CreateButton { get; } = new SetExecutorCommand();
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


        private class SetExecutorCommand : CommandBase
        {
            public override void Execute(object? parameter)
            {
                    System.Windows.Application.Current.Windows.OfType<SelectUserWindow>().First().DialogResult = true;

            }
        }

       





    }
}
