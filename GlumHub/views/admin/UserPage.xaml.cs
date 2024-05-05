using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            
            if (Application.Current.Resources["UserPageForAdminFrame"] == null)
            {
                Application.Current.Resources.Add("UserPageForAdminFrame", this.userPageForAdminFrame);
            }
            else
            {
                Application.Current.Resources["UserPageForAdminFrame"] = this.userPageForAdminFrame;
            }

            User User;
            long? userId = Application.Current.Resources["UserId"] as long?;
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                User = db.Users.Include(u => u.MasterInfo).FirstOrDefault(u => u.Id == userId);
            }
            if (User.Role == ROLES.MASTER)
            {
                Frame userPageForAdminFrame = Application.Current.Resources["UserPageForAdminFrame"] as Frame;
                userPageForAdminFrame.Navigate(new PostsPage());
            }
        }
    }
}
