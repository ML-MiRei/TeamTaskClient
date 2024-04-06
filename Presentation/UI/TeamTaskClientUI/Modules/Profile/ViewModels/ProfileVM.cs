using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.ApplicationLayer.CQRS.User.Queries.GetUserById;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Dialogs.View;
using TeamTaskClient.UI.Modules.Profile.Models;


namespace TeamTaskClient.UI.Modules.Profile.ViewModels
{
    internal class ProfileVM : ViewModelBase
    {
        private static IMediator _mediator;

        public ProfileVM(IMediator mediator, IRemoveCash removeCash)
        {

            _mediator = mediator;
            var user = mediator.Send(new GetUserByIdQuery() { UserId = Convert.ToInt32(Properties.Settings.Default.userId) }).Result;

            User = new UserModel();
            User.FirstName = user.FirstName;
            User.LastName= user.LastName;
            User.PhoneNumber = user.PhoneNumber;
            User.Email = user.Email;
            User.Tag = user.Tag;

            Logout = new LogoutCommand(removeCash);
        }


        public UserModel User { get; set; }


        public ICommand Logout { get; }
        public ICommand EditProfile { get; }

        private class EditProfileCommand() : CommandBase
        {
            public override void Execute(object? parameter)
            {
                
            }
        }


        private class LogoutCommand(IRemoveCash removeCash) : CommandBase
        {
            public override void Execute(object? parameter)
            {
                removeCash.RemoveCash();
                Properties.Settings.Default.userId = 0;
                Properties.Settings.Default.userTag = "";
                Properties.Settings.Default.Save();
                    

                App.Current.Shutdown();
            }
        }
    }
}
