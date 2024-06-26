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
    /// Логика взаимодействия для EditProfilePageClient.xaml
    /// </summary>
    public partial class EditProfilePageClient : Page
    {
        public EditProfilePageClient()
        {
            InitializeComponent();
            if (Application.Current.Resources["ProfilePhoto"] == null)
            {
                Application.Current.Resources.Add("ProfilePhoto", profilePhoto);
            }
            else
            {
                Application.Current.Resources["ProfilePhoto"] = profilePhoto;
            }
        }
    }
}
