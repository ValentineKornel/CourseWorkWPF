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

namespace GlumHub
{
    /// <summary>
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        public MasterPage()
        {
            InitializeComponent();
            if(Application.Current.Resources["MasterPageForClientFrame"] == null)
                Application.Current.Resources.Add("MasterPageForClientFrame", this.masterPageForClientFrame);
            else
                Application.Current.Resources["MasterPageForClientFrame"] = this.masterPageForClientFrame;
            if (Application.Current.Resources["MasterPageForClientFrame"] == null)

                Application.Current.Resources.Add("SubscribeUnsubscribeCommand", this.subscribeUnsubscribeCommand);
            else
                Application.Current.Resources["SubscribeUnsubscribeCommand"] = this.subscribeUnsubscribeCommand;
            

            masterPageForClientFrame.Navigate(new MsterServicePage());
        }
    }
}
