using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GlumHub
{
    class EditProfilePageMasterVM
    {
        Frame mainFrame;

        private User _user;
        public User user
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(user));
            }
        }

        byte[] _profileImage;
        public byte[] ProfileImage
        {
            get { return _profileImage; }
            set
            {
                _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        private string _bio;
        public string Bio
        {
            get { return _bio; }
            set
            {
                _bio = value;
                OnPropertyChanged(nameof(Bio));
            }
        }

        private string _businessAddress;
        public string BusinessAddress
        {
            get { return _businessAddress; }
            set
            {
                _businessAddress = value;
                OnPropertyChanged(nameof(BusinessAddress));
            }
        }

        string _firstName;
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

        string _secondName;
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

        string _email;
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

        string _tel;
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

        public EditProfilePageMasterVM()
        {
            user = Application.Current.Resources["User"] as User;
            ProfileImage = user.ProfileImage;
            if(user.MasterInfo != null)
            {
                Bio = user.MasterInfo.Bio;
                BusinessAddress = user.MasterInfo.BusinessAddress;
            }
            FirstName = user.Firstname;
            SecondName = user.Secondname;
            Email = user.Email;
            Tel = user.Tel;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand _changeProfileImageCommand;
        public ICommand ChangeProfileImageCommand
        {
            get
            {
                if (_changeProfileImageCommand == null)
                    _changeProfileImageCommand = new DelegateCommand(ChangeProfileImage);
                return _changeProfileImageCommand;
            }
        }

        public void ChangeProfileImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*|Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";


            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    byte[] imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                    ProfileImage = imageData;

                    ImageBrush profilePhoto = Application.Current.Resources["ProfilePhoto"] as ImageBrush;
                    profilePhoto.ImageSource = ByteArrayToImageConverter.LoadImageFromBytes(imageData);
                }
            }
        }

        private DelegateCommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                    _saveChangesCommand = new DelegateCommand(SaveChanges);
                return _saveChangesCommand;
            }
        }


        public void SaveChanges()
        {

            if (ValidateAll())
            {
                using (ApplicationContextDB db = new ApplicationContextDB())
                {
                    User userToChage = db.Users.Include(u => u.MasterInfo).FirstOrDefault(u => u.Id == user.Id);
                    userToChage.ProfileImage = ProfileImage;
                    userToChage.Firstname = FirstName;
                    userToChage.Secondname = SecondName;
                    userToChage.Email = Email;
                    userToChage.Tel = Tel;
                    if(userToChage.MasterInfo != null)
                    {
                        userToChage.MasterInfo.Bio = Bio;
                        userToChage.MasterInfo.BusinessAddress = BusinessAddress;
                    }
                    else
                    {
                        userToChage.MasterInfo = new MasterInfo(Bio, BusinessAddress, userToChage.Id);
                    }

                    db.SaveChanges();

                    Application.Current.Resources["User"] = userToChage;
                }
                mainFrame = Application.Current.Resources["MainFrame"] as Frame;
                mainFrame.Navigate(new MainPage());
            }
        }

        private bool validateBio()
        {
            if (string.IsNullOrWhiteSpace(Bio))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "First name cannot be empty");
                return false;
            }
            Application.Current.Dispatcher.Invoke(() => Message = "");
            return true;
        }

        private bool validateBusinessAddress()
        {
            if (string.IsNullOrWhiteSpace(BusinessAddress))
            {
                Application.Current.Dispatcher.Invoke(() => Message = "First name cannot be empty");
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
            return validateBio() && validateBusinessAddress() && validateFirstName() && validateSecondName()
                && validateEmail() && validateTel();
        }
    }
}

