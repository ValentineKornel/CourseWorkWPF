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
    }
}
