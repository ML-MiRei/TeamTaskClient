﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamTaskClient.UI.Modules.Projects.UserControls;

namespace TeamTaskClient.UI.Modules.Teams.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserTemplate.xaml
    /// </summary>
    public partial class UserTemplate : UserControl
    {
        public UserTemplate()
        {
            InitializeComponent();
        }


        public static DependencyProperty FirstNameProperty = DependencyProperty.Register("FirstName", typeof(string), typeof(UserTemplate));
        public static DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(string), typeof(UserTemplate));



        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty);}
            set { SetValue(FirstNameProperty, value); }
        }

        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
    }
}
