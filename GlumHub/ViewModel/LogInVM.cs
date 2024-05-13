using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Application = System.Windows.Application;

namespace GlumHub
{
    class LogInVM : INotifyPropertyChanged
    {
        private string _username;
        public string Username { get { return _username; } 
            set { _username = value;
                OnPropertyChanged(nameof(Username));
            } }

        private string _password;
        public string Password { get { return _password; } 
            set { _password = value;
                OnPropertyChanged(nameof(Password));
            } }


        private string _message;
        public string Message { get { return _message; }
        set { _message = value;
                OnPropertyChanged(nameof(Message));
           } }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private DelegateCommand _logInCommand;
        public ICommand LogInCommand
        {
            get
            {
                if (_logInCommand == null)
                    _logInCommand = new DelegateCommand(Login);
                return _logInCommand;
            }
        }

        private void Login()
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                Credentials cred = db.Credentials.FirstOrDefault(c => c.Username == Username);


                Password = Application.Current.Resources["Password"] as string;

                if (cred != null && cred.CheckPassword(Password)) {
                    User user = db.Users.Include(u => u.MasterInfo).Include(u => u.Masters).FirstOrDefault(u => u.Username == Username);


                    if (Application.Current.Resources["User"] == null)
                        Application.Current.Resources.Add("User", user);
                    else
                        Application.Current.Resources["User"] = user;

                    var mainFrame = Application.Current.Resources["MainFrame"] as Frame;
                    if(user.Role == ROLES.ADMIN)
                    {
                        db.Dispose();
                        mainFrame.Navigate(new MainPageAdmin());
                    }
                    else
                    {
                        db.Dispose();
                        mainFrame.Navigate(new MainPage());
                    }
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() => Message = "Неверный пароль или имя пользователя");
                }
            }
        }


        private DelegateCommand _registerRedirectCommand;
        public ICommand RegisterRedirectCommand
        {
            get
            {
                if (_registerRedirectCommand == null)
                    _registerRedirectCommand = new DelegateCommand(RegisterRedirect);
                return _registerRedirectCommand;
            }
        }

        private void RegisterRedirect()
        {
            var mainFrame = Application.Current.Resources["MainFrame"] as Frame;
            mainFrame.Navigate(new RegisterPage());

        }


    }
}
