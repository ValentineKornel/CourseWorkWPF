using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GlumHub
{
    internal class UpcomingBookingPageVM
    {
        User Master;

        private ObservableCollection<BookingWrapper> _bookedBookings;
        public ObservableCollection<BookingWrapper> BookedBookings
        {
            get { return _bookedBookings; }
            set
            {
                _bookedBookings = value;
                OnPropertyChanged(nameof(BookedBookings));
            }
        }

        public UpcomingBookingPageVM()
        {
            Master = Application.Current.Resources["User"] as User;
            BookedBookings = new ObservableCollection<BookingWrapper>();
            UpdateBookedBookings();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private DelegateCommand<long?> _deleteBookedBookingCommand;
        public ICommand DeleteBookedBookingCommand
        {
            get
            {
                if (_deleteBookedBookingCommand == null)
                    _deleteBookedBookingCommand = new DelegateCommand<long?>(DeleteBookedBooking);
                return _deleteBookedBookingCommand;
            }
        }

        private void DeleteBookedBooking(long? BookingId)
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                Booking bookingToDelete = db.Bookings.FirstOrDefault(b => b.Id == BookingId);
                db.Bookings.Remove(bookingToDelete);
                db.SaveChanges();
            }
            UpdateBookedBookings();
        }



        private void UpdateBookedBookings()
        {
            BookedBookings.Clear();
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                foreach (Booking b in db.Bookings.Include(b => b.Master).Include(b => b.Client).Where(b => b.Master.Id == Master.Id && b.Booked == true))
                {
                    BookedBookings.Add(new BookingWrapper(b, DeleteBookedBookingCommand));
                }
                BookedBookings = new ObservableCollection<BookingWrapper>(BookedBookings.OrderBy(w => w.booking.Date_Time));
            }
        }

        public class BookingWrapper
        {
            public Booking booking { get; set; }
            public ICommand DeleteBookedBookingCommand { get; set; }

            public BookingWrapper(Booking booking, ICommand DeleteBookedBookingCommand)
            {
                this.booking = booking;
                this.DeleteBookedBookingCommand = DeleteBookedBookingCommand;
            }
        }
    }
}
