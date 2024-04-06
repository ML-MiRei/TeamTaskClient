﻿using MediatR;
using System;
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

namespace TeamTaskClient.UI.Modules.Messanger.View
{
    /// <summary>
    /// Логика взаимодействия для MessangerPage.xaml
    /// </summary>
    public partial class MessangerPage : Page
    {
        public MessangerPage(IMediator mediator)
        {
            InitializeComponent();

            ChatPageLayout.NavigationService.Navigate(new ChatsListPage(mediator));
            MessagePageLayout.NavigationService.Navigate(new MessagePage(mediator));

        }
    }
}
