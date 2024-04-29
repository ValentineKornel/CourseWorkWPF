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
using System.Windows.Media.Imaging;

namespace GlumHub
{
    internal class ApplyToMasterPageVM
    {

        private User _user;

        byte[] _attachmentImage;
        public byte[] AttachmentImage
        {
            get { return _attachmentImage; }
            set
            {
                _attachmentImage = value;
                OnPropertyChanged(nameof(AttachmentImage));
            }
        }

        private string _attachmentLetter;
        public string AttachmentLetter
        {
            get { return _attachmentLetter; }
            set
            {
                _attachmentLetter = value;
                OnPropertyChanged(nameof(AttachmentLetter));
            }
        }


        public ApplyToMasterPageVM()
        {
            _user = Application.Current.Resources["User"] as User;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private DelegateCommand _loadImageCommand;
        public ICommand LoadImageCommand
        {
            get
            {
                if (_loadImageCommand == null)
                    _loadImageCommand = new DelegateCommand(LoadImage);
                return _loadImageCommand;
            }
        }

        public void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    byte[] imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                    AttachmentImage = imageData;
                }
                Image img = Application.Current.Resources["AttachmentImage"] as Image;
                img.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }


        private DelegateCommand _applyCommand;
        public ICommand ApplyCommand
        {
            get
            {
                if (_applyCommand == null)
                    _applyCommand = new DelegateCommand(Apply);
                return _applyCommand;
            }
        }

        public void Apply()
        {
            Request request = new Request(_user.Id, AttachmentImage, AttachmentLetter);
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                db.Requests.Add(request);
                db.SaveChanges();
            }

            Notification.SendNotification(_user.Id, "Ваша заявка была принята к рассморению. После принятия решения вы будете оповещены");
            Frame homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new NotificationsPage());
        }
    }
}
