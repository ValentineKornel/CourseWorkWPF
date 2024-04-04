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
    class FreeBookingPageVM
    {
        User Master;
        Frame homePageMasterFrame;

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

        public FreeBookingPageVM()
        {
            Master = Application.Current.Resources["User"] as User;
            FreeBookings = new ObservableCollection<BookingWrapper>();
            UpdateFreeBookings();   
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DelegateCommand _newBookingPageRedirectCommand;
        public ICommand NewBookingPageRedirectCommand
        {
            get
            {
                if (_newBookingPageRedirectCommand == null)
                    _newBookingPageRedirectCommand = new DelegateCommand(NewBookingPageRedirect);
                return _newBookingPageRedirectCommand;
            }
        }

        private void NewBookingPageRedirect()
        {
            homePageMasterFrame = Application.Current.Resources["HomePageMasterFrame"] as Frame;
            homePageMasterFrame.Navigate(new NewBookingPage());
        }


        private DelegateCommand<long?> _deleteFreeBookingCommand;
        public ICommand DeleteFreeBookingCommand
        {
            get
            {
                if (_deleteFreeBookingCommand == null)
                    _deleteFreeBookingCommand = new DelegateCommand<long?>(DeleteFreeBooking);
                return _deleteFreeBookingCommand;
            }
        }

        private void DeleteFreeBooking(long? BookingId)
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                Booking bookingToDelete = db.Bookings.FirstOrDefault(b => b.Id == BookingId);
                db.Bookings.Remove(bookingToDelete);
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
                    FreeBookings.Add(new BookingWrapper(b, DeleteFreeBookingCommand));
                }
                FreeBookings = new ObservableCollection<BookingWrapper>(FreeBookings.OrderBy(w => w.booking.Date_Time));
                
            }
        }

        public class BookingWrapper
        {
            public Booking booking { get; set; }
            public ICommand DeleteFreeBookingCommand { get; set; }

            public BookingWrapper(Booking booking, ICommand DeleteFreeBookingCommand)
            {
                this.booking = booking;
                this.DeleteFreeBookingCommand = DeleteFreeBookingCommand;
            }
        }


        

    }
}

