using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.UI.Modules.Profile.Models
{
    internal class UserModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }

        public string FullName => FirstName + " " + SecondName + " " + LastName;
        public string FormatPhoneNumber => PhoneNumber;

    }
}
