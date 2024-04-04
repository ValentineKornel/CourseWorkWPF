using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GlumHub
{
    class RegisterVM : INotifyPropertyChanged
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                validateUsername();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                validateFirstName();
            }
        }

        private string _secondName;
        public string SecondName
        {
            get { return _secondName; }
            set
            {
                _secondName = value;
                OnPropertyChanged(nameof(SecondName));
                validateSecondName();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                validateEmail();
            }
        }

        private string _tel;
        public string Tel
        {
            get { return _tel; }
            set
            {
                _tel = value;
                OnPropertyChanged(nameof(Tel));
                validateTel();
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DelegateCommand _registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                    _registerCommand = new DelegateCommand(Register);
                return _registerCommand;
            }
        }

        private void Register()
        {
            User user = new User(Username, FirstName, SecondName, Email, Tel);
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                bool isUsernameTaken = db.Users.Any(u => u.Username == Username);
                if (isUsernameTaken)
                {
                    Application.Current.Dispatcher.Invoke(() => Message = "This username is already taken");
                    return;
                }

                if (ValidateAll())
                {
                    using(FileStream fs = new FileStream("D:\\Study\\CourseWork\\GlumHub\\GlumHub\\files\\defaultUser.png", FileMode.Open))
                    {
                        byte[] imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                        user.ProfileImage = imageData;
                    }
                    db.Users.Add(user);
                    db.SaveChanges();
                    Application.Current.Dispatcher.Invoke(() => Message = "You have been registred");

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
                    Credentials credentials = new Credentials(user.Username, hashedPassword, user.Id);
                    db.Credentials.Add(credentials);
                    db.SaveChanges();
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() => Message = "You shuld fill all information correcty");
                }
            }
        }

        private DelegateCommand _loginRedirectCommand;
        public ICommand LoginRedirectCommand
        {
            get
            {
                if (_loginRedirectCommand == null)
                    _loginRedirectCommand = new DelegateCommand(LoginRedirect);
                return _loginRedirectCommand;
            }
        }

        private void LoginRedirect()
        {
            var mainFrame = Application.Current.Resources["MainFrame"] as Frame;
            mainFrame.Navigate(new LoginPage());
        }
        

        private bool validateUsername()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "Username cannot be empty");
                return false;
            }
            Application.Current.Dispatcher.Invoke(() => Message = "");

            return true;
        }

        private bool validateFirstName()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "First name cannot be empty");
                return false;
            }
            else if (!Regex.IsMatch(FirstName, @"^[a-zA-Zа-яА-Я]+$"))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "First name can only contain letters");
                return false;
            }
            Application.Current.Dispatcher.Invoke(() => Message = "");
            return true;
        }

        private bool validateSecondName()
        {
            if (string.IsNullOrWhiteSpace(SecondName))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "Second name cannot be empty");
                return false;
            }
            else if (!Regex.IsMatch(SecondName, @"^[a-zA-Zа-яА-Я]+$"))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "Second name can only contain letters");
                return false;
            }
            Application.Current.Dispatcher.Invoke(() => Message = "");
            return true;
        }

        private bool validateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "Email cannot be empty");
                return false;
            }
            else if (!Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "Invalid email format");
                return false;
            }
            Application.Current.Dispatcher.Invoke(() => Message = "");
            return true;
        }

        private bool validateTel()
        {
            if (string.IsNullOrWhiteSpace(Tel))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "Ielephone number cannot be empty");
                return false;
            }
            else if (!Regex.IsMatch(Tel, @"^\+?[0-9]{1,3}[\s-]?\(?[0-9]{3}\)?[\s-]?[0-9]{3}[\s-]?[0-9]{2}[\s-]?[0-9]{2}$"))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "IInvalid telephone number format");
                return false;
            }
            Application.Current.Dispatcher.Invoke(() => Message = "");
            return true;
        }

        private bool ValidateAll()
        {
            return validateUsername() && validateFirstName() && validateSecondName()
                && validateEmail() && validateTel();
        }
    }
}
