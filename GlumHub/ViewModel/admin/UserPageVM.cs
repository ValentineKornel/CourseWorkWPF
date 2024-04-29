using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GlumHub
{
    internal class UserPageVM
    {

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



        public UserPageVM()
        {
            long? userId = Application.Current.Resources["UserId"] as long?;
            using(ApplicationContextDB db = new ApplicationContextDB())
            {
                User = db.Users.Include(u => u.MasterInfo).FirstOrDefault(u => u.Id == userId);

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand _deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get
            {
                if (_deleteUserCommand == null)
                    _deleteUserCommand = new DelegateCommand(DeleteUser);
                return _deleteUserCommand;
            }
        }

        private void DeleteUser()
        {

            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                User userToDelete = db.Users.Include(m => m.MasterInfo).FirstOrDefault(u => u.Id == User.Id);
                List<UserRelation> relations = db.UserRelations.Where(r => r.MasterId == User.Id || r.FollowerId == User.Id).ToList();
                foreach(UserRelation r in relations)
                {
                    db.UserRelations.Remove(r);
                }

                List<Request> requests = db.Requests.Include(r => r.Client).Where(r => r.Clientid == userToDelete.Id).ToList();
                foreach(Request r in requests)
                {
                    db.Requests.Remove(r);
                }
                List<Booking> bookingsAsMaster = db.Bookings.Include(b => b.Master).Where(b => b.MasterId == userToDelete.Id).ToList();
                foreach(Booking b in bookingsAsMaster)
                {
                    Notification.SendNotification((long)b.Clientid, "Ваша запись к мастеру " + b.Master.Firstname + " " + b.Master.Secondname + " бала отменена, так как этот мастер был заблокирован");
                    db.Remove(b);
                }
                List<Booking> bookingsAsClient = db.Bookings.Include(b => b.Client).Where(b => b.Clientid == userToDelete.Id).ToList();
                foreach (Booking b in bookingsAsClient)
                {
                    Notification.SendNotification((long)b.MasterId, "Запись пользователя " + b.Client.Firstname + " " + b.Client.Secondname + " бала отменена, так как этот пользователь был заблокирован");
                    db.Remove(b);
                }

                db.Users.Remove(userToDelete);

                db.SaveChanges();
                Frame homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
                homePageFrame.Navigate(new SearchPageAdmin());
            }
        }
    }
}
