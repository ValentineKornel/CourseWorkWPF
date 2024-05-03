using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GlumHub
{
    class EditProfilePageClientVM
    {
        Frame mainFrame;

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

        public EditProfilePageClientVM()
        {
            _user = Application.Current.Resources["User"] as User;
            _profileImage = User.ProfileImage;
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
                if(_changeProfileImageCommand == null)
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
                    profilePhoto.ImageSource = LoadImageFromBytes(imageData);
                }
                
            }
        }

        private DelegateCommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if(_saveChangesCommand == null)
                    _saveChangesCommand = new DelegateCommand(SaveChanges);
                return _saveChangesCommand;
            }
        }

        public void SaveChanges()
        {
            using(ApplicationContextDB db = new ApplicationContextDB())
            {
                User userToChage = db.Users.FirstOrDefault(u => u.Id == User.Id);
                userToChage.ProfileImage = ProfileImage;
                db.SaveChanges();

                Application.Current.Resources["User"] = userToChage;
            }
            mainFrame = Application.Current.Resources["MainFrame"] as Frame;
            mainFrame.Navigate(new MainPage());
        }


        private DelegateCommand _applyToMasterRedirectCommand;
        public ICommand ApplyToMasterRedirectCommand
        {
            get
            {
                if (_applyToMasterRedirectCommand == null)
                    _applyToMasterRedirectCommand = new DelegateCommand(ApplyToMasterRedirect);
                return _applyToMasterRedirectCommand;
            }
        }

        public void ApplyToMasterRedirect()
        {
            Frame homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new ApplyToMasterPage());
        }




        private BitmapImage LoadImageFromBytes(byte[] imageData)
        {
            BitmapImage bitmap = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(imageData))
            {
                stream.Position = 0;
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                bitmap.Freeze();
            }

            return bitmap;
        }
    }
}
