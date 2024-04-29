using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace GlumHub
{
    internal class NotificationsPageVM
    {

        User _user;

        private ObservableCollection<NotificationWrapper> _notifications;
        public ObservableCollection<NotificationWrapper> Notifications
        {
            get { return _notifications; }
            set
            {
                _notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }

        public NotificationsPageVM()
        {
            _user = Application.Current.Resources["User"] as User;
            Notifications = new ObservableCollection<NotificationWrapper>();
            using(ApplicationContextDB db = new ApplicationContextDB())
            {
                List<Notification> temp = (from Notification notification in db.Notifications.Where(n => n.Receiverid == _user.Id)
                                           select notification).ToList();
                temp = temp.OrderByDescending(n => n.Created).ToList();
                foreach(Notification not in temp)
                {
                    Notifications.Add(new NotificationWrapper(not, WatchNotificationCommand));
                }
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand<long?> _watchNotificationCommand;
        public ICommand WatchNotificationCommand
        {
            get
            {
                if (_watchNotificationCommand == null)
                    _watchNotificationCommand = new DelegateCommand<long?>(WatchNotification);
                return _watchNotificationCommand;
            }
        }

        private void WatchNotification(long? NotificationId)
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                Notification notification = db.Notifications.FirstOrDefault(n => n.Id == NotificationId);
                notification.Watched = true;
                db.SaveChanges();
            }
            UpdateNotifications();
        }



        private void UpdateNotifications()
        {
            Notifications.Clear();
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                List<Notification> temp = (from Notification notification in db.Notifications.Where(n => n.Receiverid == _user.Id)
                                           select notification).ToList();
                temp = temp.OrderByDescending(n => n.Created).ToList();
                foreach (Notification not in temp)
                {
                    Notifications.Add(new NotificationWrapper(not, WatchNotificationCommand));
                }
            }
        }




        public class NotificationWrapper
        {
            public Notification notification { get; set; }
            public ICommand WatchNotificationCommand { get; set; }
            public string HiddenPropertyText { get; set; }

            public NotificationWrapper(Notification notification, ICommand WatchNotificationCommand)
            {
                this.notification = notification;
                this.WatchNotificationCommand = WatchNotificationCommand;
                if(notification.Watched == false)
                {
                    HiddenPropertyText = "Visible";
                }
                else
                {
                    HiddenPropertyText = "Hidden";
                }
            }
        }
    }
}
