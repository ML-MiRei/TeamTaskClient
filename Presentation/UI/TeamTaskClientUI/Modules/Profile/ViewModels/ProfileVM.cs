using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamTaskClient.Infrastructure.Services.Interfaces;
using TeamTaskClient.UI.Common.Base;
using TeamTaskClient.UI.Modules.Profile.Models;


namespace TeamTaskClient.UI.Modules.Profile.ViewModels
{
    internal class ProfileVM : ViewModelBase
    {

        public ProfileVM()
        {
            User = new UserModel();
            User.FirstName = "Miko";
            User.LastName= "Kurosaki";
            User.PhoneNumber = "+7-980-495-35-35";
            User.Email = "bleach@gmail.com";
            User.Tag = "svgewbmemwobmwmboe";
        }


        public UserModel User { get; set; }


        public ICommand Logout { get; }
    }
}
