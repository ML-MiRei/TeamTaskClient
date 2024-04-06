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
using System.Windows.Shapes;
using TeamTaskClient.UI.Dialogs.ViewModels;

namespace TeamTaskClient.UI.Dialogs.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class InputDialogWindow : Window
    {
        static InputDialogVM vM;

        public InputDialogWindow(string textDialog, string contentButton, List<string> actions)
        {
            InitializeComponent();
            vM = new InputDialogVM(textDialog, contentButton, actions);
            DataContext = vM;
        }

        public List<string> GetInputValue()
        {

             return vM.InputValues.Select(upp => upp.Text).ToList();

        }
    }
}