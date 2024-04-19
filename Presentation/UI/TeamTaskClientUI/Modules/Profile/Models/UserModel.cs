﻿using System.Text.RegularExpressions;

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

        public string FullName => FirstName + " " + SecondName;
        public string Lit => FirstName[0] + "";

        public string FormatedPhoneNumber => Regex.Replace(PhoneNumber, @"(\d{1})(\d{3})(\d{3})(\d{2})(\d{2})", "+$1-($2)-$3-$4-$5");


    }
}
