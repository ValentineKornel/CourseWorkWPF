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
    /// Логика взаимодействия для HomePageMaster.xaml
    /// </summary>
    public partial class HomePageMaster : Page
    {
        public HomePageMaster()
        {
            InitializeComponent();
            if (Application.Current.Resources["HomePageMasterFrame"] == null)
                Application.Current.Resources.Add("HomePageMasterFrame", this.homePageMasterFrame);
            else
                Application.Current.Resources["HomePageMasterFrame"] = this.homePageMasterFrame;
            homePageMasterFrame.Navigate(new AsMasterPage());
        }
    }
}
