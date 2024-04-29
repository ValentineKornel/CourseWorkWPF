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

namespace GlumHub
{
    /// <summary>
    /// Логика взаимодействия для MainPageAdmin.xaml
    /// </summary>
    public partial class MainPageAdmin : Page
    {
        public MainPageAdmin()
        {
            InitializeComponent();
            if (Application.Current.Resources.Contains("HomePageFrame"))
            {
                Application.Current.Resources["HomePageFrame"] = this.homePageFrame;
            }
            else
            {
                Application.Current.Resources.Add("HomePageFrame", this.homePageFrame);
            }

            homePageFrame.Navigate(new HomePageAdmin());
        }
    }
}
