using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GlumHub
{
    internal class RequestPageVM
    {

        private Request _request;

        ImageSource _attachmentImage;
        public ImageSource AttachmentImage
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

        public RequestPageVM()
        {
            long? requestId = Application.Current.Resources["ReuestId"] as long?;
            using(ApplicationContextDB db = new ApplicationContextDB())
            {
                _request = db.Requests.Include(r => r.Client).FirstOrDefault(r => r.Id == requestId);
            }

            AttachmentImage = LoadImageFromBytes(_request.AttachmentImage);
            AttachmentLetter = _request.AttachmentLetter;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private DelegateCommand _confirmCommand;
        public ICommand ConfirmCommand
        {
            get
            {
                if (_confirmCommand == null)
                    _confirmCommand = new DelegateCommand(Confirm);
                return _confirmCommand;
            }
        }

        public void Confirm()
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                Request req = db.Requests.FirstOrDefault(r => r.Id == _request.Id);
                db.Requests.Remove(req);


                User userToChange = db.Users.FirstOrDefault(u => u.Id ==  _request.Clientid);
                userToChange.Role = ROLES.MASTER;

                db.SaveChanges();
            }
            Notification.SendNotification(_request.Clientid, "Ваша заявка на статус мастера была одобрена.\nДобавьте соответсвующую информацию в ваш профиль");
            Frame homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new HomePageAdmin());
        }

        private DelegateCommand _rejectCommand;
        public ICommand RejectCommand
        {
            get
            {
                if (_rejectCommand == null)
                    _rejectCommand = new DelegateCommand(Reject);
                return _rejectCommand;
            }
        }

        public void Reject()
        {
            using(ApplicationContextDB db = new ApplicationContextDB())
            {
                Request req = db.Requests.FirstOrDefault(r => r.Id == _request.Id);
                db.Requests.Remove(req);
                db.SaveChanges();
            }

            Notification.SendNotification(_request.Clientid, "Ваша заявка на статус мастера была отклонена");
            Frame homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            homePageFrame.Navigate(new HomePageAdmin());
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
