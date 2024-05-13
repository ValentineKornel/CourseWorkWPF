using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GlumHub
{
    internal class MainPageAdminVM : INotifyPropertyChanged
    {
        Frame mainFrame;
        Frame homePageFrame;

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public MainPageAdminVM()
        {
            _user = Application.Current.Resources["User"] as User;
            mainFrame = Application.Current.Resources["MainFrame"] as Frame;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand _homeRedirectCommand;
        public ICommand HomePageRedirectCommand
        {
            get
            {
                if (_homeRedirectCommand == null)
                    _homeRedirectCommand = new DelegateCommand(HomePageRedirect);
                return _homeRedirectCommand;
            }
        }

        private void HomePageRedirect()
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new HomePageAdmin());
            
        }


        private DelegateCommand _searchPageRedirectCommand;
        public ICommand SearchPageRedirectCommand
        {
            get
            {
                if (_searchPageRedirectCommand == null)
                    _searchPageRedirectCommand = new DelegateCommand(searchPageRedirect);
                return _searchPageRedirectCommand;
            }
        }

        private void searchPageRedirect()
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new SearchPageAdmin());
        }


        private DelegateCommand _settingsRedirectCommand;
        public ICommand SettingsPageRedirectCommand
        {
            get
            {
                if (_settingsRedirectCommand == null)
                    _settingsRedirectCommand = new DelegateCommand(SettingsPageRedirect);
                return _settingsRedirectCommand;
            }
        }

        private void SettingsPageRedirect()
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new SettingsPage());
        }

        private DelegateCommand _logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                if (_logOutCommand == null)
                    _logOutCommand = new DelegateCommand(LogOut);
                return _logOutCommand;
            }
        }

        private void LogOut()
        {
            mainFrame.Navigate(new LoginPage());
        }
    }
}
