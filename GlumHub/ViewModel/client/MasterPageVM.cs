using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static GlumHub.FreeBookingPageVM;

namespace GlumHub
{
    class MasterPageVM
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
                User user = db.Users.FirstOrDefault(u => u.Id == MasterId);
                if (user.MasterInfo == null)
                {
                    user.MasterInfo = db.MasterInfos.FirstOrDefault(i => i.UserId == MasterId);
                }
                Master = user;
                //Application.Current.Resources.Remove("MasterId");
            }
            FreeBookings = new ObservableCollection<BookingWrapper>();
            UpdateFreeBookings();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                Booking bookingToBook = db.Bookings.FirstOrDefault(b => b.Id == BookingId);
                bookingToBook.Client = User;
                bookingToBook.Clientid = User.Id;
                bookingToBook.Booked = true;
                db.SaveChanges();
            }
            UpdateFreeBookings();
        }


        private void UpdateFreeBookings()
        {
            FreeBookings.Clear();
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                foreach (Booking b in db.Bookings.Include(b => b.Master).Where(b => b.Master.Id == Master.Id && b.Booked == false))
                {
                    FreeBookings.Add(new BookingWrapper(b, BookFreeBookingCommand));
                }
            }
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
