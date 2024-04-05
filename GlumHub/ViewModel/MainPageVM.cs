﻿using Prism.Commands;
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
    class MainPageVM
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

        public MainPageVM()
        {
            _user = Application.Current.Resources["User"] as User;
            mainFrame = Application.Current.Resources["MainFrame"] as Frame;
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DelegateCommand _myProfileRedirectCommand;
        public ICommand MyProfileRedirectCommand
        {
            get
            {
                if (_myProfileRedirectCommand == null)
                    _myProfileRedirectCommand = new DelegateCommand(MyProfileRedirect);
                return _myProfileRedirectCommand;
            }
        }

        private void MyProfileRedirect()
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            if (_user.Role == ROLES.CLIENT)
                homePageFrame.Navigate(new MyProfilePageClient());
            else
                homePageFrame.Navigate(new MyProfilePageMaster());
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
            if (_user.Role == ROLES.CLIENT)
                homePageFrame.Navigate(new HomePageClient());
            else
                homePageFrame.Navigate(new HomePageMaster());
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
            homePageFrame.Navigate(new SearchPage());
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


        private DelegateCommand _forLabsPageRedirectCommand;
        public ICommand ForLabsPageRedirectCommand
        {
            get
            {
                if (_forLabsPageRedirectCommand == null)
                    _forLabsPageRedirectCommand = new DelegateCommand(ForLabsPageRedirect);
                return _forLabsPageRedirectCommand;
            }
        }

        private void ForLabsPageRedirect()
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new ForLabsPage());
        }
    }
}