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

namespace GlumHub.views
{
    /// <summary>
    /// Логика взаимодействия для MyMessageBox.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        public MyMessageBox()
        {
            InitializeComponent();
        }

        public MyMessageBox(string message)
        {
            InitializeComponent();
            this.message.Content = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}
