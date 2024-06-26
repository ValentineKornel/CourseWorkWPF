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
    /// Логика взаимодействия для MyProfilePageMaster.xaml
    /// </summary>
    public partial class MyProfilePageMaster : Page
    {
        public MyProfilePageMaster()
        {
            InitializeComponent();
            if (Application.Current.Resources["MyProfilePageMasterFrame"] == null)
                Application.Current.Resources.Add("MyProfilePageMasterFrame", this.myProfilePageMasterFrame);
            else
                Application.Current.Resources["MyProfilePageMasterFrame"] = this.myProfilePageMasterFrame;

            myProfilePageMasterFrame.Navigate(new PostsPage());
        }
    }
}
