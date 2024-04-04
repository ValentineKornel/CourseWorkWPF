using Microsoft.VisualBasic.ApplicationServices;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
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

            var user = Application.Current.Resources["User"] as User;
            if (user.Role == ROLES.CLIENT)
                homePageFrame.Navigate(new HomePageClient());
            else
                homePageFrame.Navigate(new HomePageMaster());
            //homePageFrame.Navigate(new MyProfilePage());
        }
    }
}
