using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static GlumHub.FreeBookingPageVM;

namespace GlumHub
{
    class MasterPageVM : INotifyPropertyChanged
    {
        private long? _masterId;
        public long? MasterId
        {
            get { return _masterId; }
            set
            {
                _masterId = value;
                OnPropertyChanged(nameof(MasterId));
            }
        }

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

        private User _master;
        public User Master
        {
            get { return _master; }
            set
            {
                _master = value;
                OnPropertyChanged(nameof(Master));
            }
        }

        private string _subscribeButtonContent;
        public string SubscriptionButtonContent
        {
            get { return _subscribeButtonContent; }
            set
            {
                _subscribeButtonContent = value;
                OnPropertyChanged(nameof(SubscriptionButtonContent));
            }
        }

        private ObservableCollection<BookingWrapper> _freeBookings;
        public ObservableCollection<BookingWrapper> FreeBookings
        {
            get { return _freeBookings; }
            set
            {
                _freeBookings = value;
                OnPropertyChanged(nameof(FreeBookings));
            }
        }


        public MasterPageVM()
        {
            User = Application.Current.Resources["User"] as User;
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                
                MasterId = Application.Current.Resources["MasterId"] as long?;
                User master = db.Users.FirstOrDefault(u => u.Id == MasterId);
                if (master.MasterInfo == null)
                {
                    master.MasterInfo = db.MasterInfos.FirstOrDefault(i => i.UserId == MasterId);
                }
                Master = master;


                User = db.Users.Include(u => u.MasterInfo).Include(u => u.Masters).FirstOrDefault(u => u.Id == User.Id);
                UserRelation relation = db.UserRelations.Include(u => u.Master).Include(u => u.Follower).FirstOrDefault(u => u.MasterId == Master.Id && u.FollowerId == User.Id);
                if (db.UserRelations.Contains(relation))
                {
                    SubscriptionButtonContent = "Unsubscribe";
                }
                else
                {
                    SubscriptionButtonContent = "Subscribe";
                }


            }
            FreeBookings = new ObservableCollection<BookingWrapper>();
            UpdateFreeBookings();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private DelegateCommand _subscribeUnsubscribeCommand;
        public ICommand SubscribeUnsubscribeCommand
        {
            get
            {
                if (_subscribeUnsubscribeCommand == null)
                    _subscribeUnsubscribeCommand = new DelegateCommand(SubscribeUnsubscribe);
                return _subscribeUnsubscribeCommand;
            }
        }

        private void SubscribeUnsubscribe()
        {
            Button btn = Application.Current.Resources["SubscribeUnsubscribeCommand"] as Button;
            
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                User masterToChange = db.Users.Include(m => m.MasterInfo).FirstOrDefault(u => u.Id == MasterId);
                User userToChange = db.Users.Include(u => u.MasterInfo).Include(u => u.Masters).FirstOrDefault(u => u.Id == User.Id);
                if (SubscriptionButtonContent == "Subscribe")
                {
                    btn.Content = "Unsubscribe";
                    //userToChange.Masters.Add(masterToChange);
                    userToChange.Masters.Add(new UserRelation(userToChange.Id, userToChange, masterToChange.Id, masterToChange));
                }
                else
                {
                    btn.Content = "Subscribe";
                    //userToChange.Masters.Remove(masterToChange);
                    UserRelation relation = db.UserRelations.Include(u => u.Master).Include(u => u.Follower).FirstOrDefault(u => u.MasterId == Master.Id && u.FollowerId == User.Id);
                    userToChange.Masters.Remove(relation);
                }
                db.SaveChanges();

                User = db.Users.Include(u => u.MasterInfo).Include(u => u.Masters).FirstOrDefault(u => u.Id == User.Id);
            }
        }



        private DelegateCommand<long?> _bookFreeBookingCommand;
        public ICommand BookFreeBookingCommand
        {
            get
            {
                if (_bookFreeBookingCommand == null)
                    _bookFreeBookingCommand = new DelegateCommand<long?>(BookFreeBooking);
                return _bookFreeBookingCommand;
            }
        }

        private void BookFreeBooking(long? BookingId)
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                Booking bookingToBook = db.Bookings.Include(b => b.Client).Include(b => b.Master).Include(b => b.Master.MasterInfo).FirstOrDefault(b => b.Id == BookingId);
                //bookingToBook.Client = User;
                bookingToBook.Clientid = User.Id;
                bookingToBook.Booked = true;
                db.SaveChanges();


                bookingToBook = db.Bookings.Include(b => b.Client).Include(b => b.Master).Include(b => b.Master.MasterInfo).FirstOrDefault(b => b.Id == BookingId);

                string googleCalendarUrlForClient = Notification.GenerateGoogleCalendarUrlForClient(bookingToBook);
                string messageHtmlForClient = $@"
                <h2>Thank you for booking!</h2>
                <p>Event Details:</p>
                <ul>
                    <li>Date & Time: {bookingToBook.Date_Time}</li>
                    <li>Service: {bookingToBook.Service}</li>
                    <li>Master: {bookingToBook.Master.Firstname} {bookingToBook.Master.Secondname}</li>
                </ul>
                <p><a href=""{googleCalendarUrlForClient}"">Add to Google Calendar</a></p>
                ";
                Notification.SendEmailNotification(bookingToBook.Client.Id, messageHtmlForClient, "html");



                string googleCalendarUrlForMaster = Notification.GenerateGoogleCalendarUrlForMaster(bookingToBook);
                string messageHtmlForMaster = $@"
                <h2>You have new booking!</h2>
                <p>Event Details:</p>
                <ul>
                    <li>Date & Time: {bookingToBook.Date_Time}</li>
                    <li>Service: {bookingToBook.Service}</li>
                    <li>Client: {bookingToBook.Client.Firstname} {bookingToBook.Client.Secondname}</li>
                </ul>
                <p><a href=""{googleCalendarUrlForMaster}"">Add to Google Calendar</a></p>
                ";
                Notification.SendEmailNotification(bookingToBook.Master.Id, messageHtmlForMaster, "html");
            }

            
            UpdateFreeBookings();
        }

        


        private void UpdateFreeBookings()
        {
            FreeBookings.Clear();
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                foreach (Booking b in db.Bookings.Include(b => b.Master).Where(b => b.Master.Id == Master.Id && b.Booked == false && b.Date_Time > DateTime.Now))
                {
                    FreeBookings.Add(new BookingWrapper(b, BookFreeBookingCommand));
                }
                FreeBookings = new ObservableCollection<BookingWrapper>(FreeBookings.OrderBy(w => w.booking.Date_Time));
            }
        }


        private DelegateCommand _serviceRedirectCommand;
        public ICommand ServicePageRedirectCommand
        {
            get
            {
                if (_serviceRedirectCommand == null)
                    _serviceRedirectCommand = new DelegateCommand(ServicePageRedirect);
                return _serviceRedirectCommand;
            }
        }

        public void ServicePageRedirect()
        {
            Frame masterPageForClientFrame = Application.Current.Resources["MasterPageForClientFrame"] as Frame;
            masterPageForClientFrame.Navigate(new MasterServicePage());
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

        public void PostsPageRedirect()
        {
            Frame masterPageForClientFrame = Application.Current.Resources["MasterPageForClientFrame"] as Frame;
            masterPageForClientFrame.Navigate(new PostsPage());
        }


        public class BookingWrapper
        {
            public Booking booking { get; set; }
            public ICommand BookFreeBookingCommand { get; set; }

            public BookingWrapper(Booking booking, ICommand DeleteFreeBookingCommand)
            {
                this.booking = booking;
                BookFreeBookingCommand = DeleteFreeBookingCommand;
            }
        }


    }
}
