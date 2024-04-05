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
using System.Windows.Input;

namespace GlumHub
{
    internal class HistoryPageMasterVM
    {

        User Master;

        private ObservableCollection<BookingWrapper> _historyBookings;
        public ObservableCollection<BookingWrapper> HistoryBookings
        {
            get { return _historyBookings; }
            set
            {
                _historyBookings = value;
                OnPropertyChanged(nameof(HistoryBookings));
            }
        }

        public HistoryPageMasterVM()
        {
            Master = Application.Current.Resources["User"] as User;
            HistoryBookings = new ObservableCollection<BookingWrapper>();
            UpdateHistoryBookings();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private DelegateCommand<long?> _deleteHistoryBookingCommand;
        public ICommand DeleteHisotyBookingCommand
        {
            get
            {
                if (_deleteHistoryBookingCommand == null)
                    _deleteHistoryBookingCommand = new DelegateCommand<long?>(DeleteHistoryBooking);
                return _deleteHistoryBookingCommand;
            }
        }

        private void DeleteHistoryBooking(long? BookingId)
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                Booking bookingToDelete = db.Bookings.FirstOrDefault(b => b.Id == BookingId);
                db.Bookings.Remove(bookingToDelete);
                db.SaveChanges();
            }
            UpdateHistoryBookings();
        }



        private void UpdateHistoryBookings()
        {
            HistoryBookings.Clear();
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                foreach (Booking b in db.Bookings.Include(b => b.Master).Include(b => b.Client).
                    Where(b => b.Master.Id == Master.Id && b.Booked == true && b.Date_Time < DateTime.Now))
                {
                    HistoryBookings.Add(new BookingWrapper(b, DeleteHisotyBookingCommand));
                }
                HistoryBookings = new ObservableCollection<BookingWrapper>(HistoryBookings.OrderBy(w => w.booking.Date_Time));
            }
        }

        public class BookingWrapper
        {
            public Booking booking { get; set; }
            public ICommand DeleteHistoryBookingCommand { get; set; }

            public BookingWrapper(Booking booking, ICommand DeleteHistoryBookingCommand)
            {
                this.booking = booking;
                this.DeleteHistoryBookingCommand = DeleteHistoryBookingCommand;
            }
        }
    }
}
