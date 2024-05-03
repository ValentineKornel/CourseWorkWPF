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
    class MyProfilePageMesterVM
    {
        Frame homePageFrame;
        Frame myProfilePageMasterFrame;

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

        public MyProfilePageMesterVM()
        {
            _user = Application.Current.Resources["User"] as User;
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DelegateCommand _editProfileRedirectCommand;
        public ICommand EditProfileRedirectCommand
        {
            get
            {
                if (_editProfileRedirectCommand == null)
                    _editProfileRedirectCommand = new DelegateCommand(EditProfileRedirect);
                return _editProfileRedirectCommand;
            }
        }

        private void EditProfileRedirect()
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new EditProfilePageMaster());

        }


        private DelegateCommand _postsPageRedirectCommand;
        public ICommand PostsPageRedirectCommand
        {
            get
            {
                if (_postsPageRedirectCommand == null)
                    _postsPageRedirectCommand = new DelegateCommand(PostsPageRedirect);
                return _postsPageRedirectCommand;
            }
        }

        private void PostsPageRedirect()
        {
            myProfilePageMasterFrame = Application.Current.Resources["MyProfilePageMasterFrame"] as Frame;
            myProfilePageMasterFrame.Navigate(new PostsPage());

        }


        private DelegateCommand _statisticPageRedirectCommand;
        public ICommand StatisticPageRedirectCommand
        {
            get
            {
                if (_statisticPageRedirectCommand == null)
                    _statisticPageRedirectCommand = new DelegateCommand(StatisticPageRedirect);
                return _statisticPageRedirectCommand;
            }
        }

        private void StatisticPageRedirect()
        {
            myProfilePageMasterFrame = Application.Current.Resources["MyProfilePageMasterFrame"] as Frame;
            myProfilePageMasterFrame.Navigate(new StatisticPage());

        }


        private DelegateCommand _historyAsMasterPageRedirectCommand;
        public ICommand HistoryAsMasterPageRedirectCommand
        {
            get
            {
                if (_historyAsMasterPageRedirectCommand == null)
                    _historyAsMasterPageRedirectCommand = new DelegateCommand(HistoryAsMasterPageRedirect);
                return _historyAsMasterPageRedirectCommand;
            }
        }

        private void HistoryAsMasterPageRedirect()
        {
            myProfilePageMasterFrame = Application.Current.Resources["MyProfilePageMasterFrame"] as Frame;
            myProfilePageMasterFrame.Navigate(new HistoryPageMaster());

        }


        private DelegateCommand _historyAsClientPageRedirectCommand;
        public ICommand HistoryAsClientPageRedirectCommand
        {
            get
            {
                if (_historyAsClientPageRedirectCommand == null)
                    _historyAsClientPageRedirectCommand = new DelegateCommand(HistoryAsClientPageRedirect);
                return _historyAsClientPageRedirectCommand;
            }
        }

        private void HistoryAsClientPageRedirect()
        {
            myProfilePageMasterFrame = Application.Current.Resources["MyProfilePageMasterFrame"] as Frame;
            myProfilePageMasterFrame.Navigate(new HistoryPageClient());

        }
    }
}
