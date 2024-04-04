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
using System.Windows.Controls;
using System.Windows.Input;

namespace GlumHub
{
    class NewBookingPageVM
    {

        Frame homePageMasterFrame;
        User Master;


        private DateTime _dateTime;
        public DateTime Date_Time { get { return _dateTime; } 
            set {
                _dateTime = value;
                OnPropertyChanged(nameof(Date_Time));
            } 
        }

        private string _service;
        public string Service { get { return _service; } 
            set { 
                _service = value;
                OnPropertyChanged(nameof(Service));
            }
        }


        public NewBookingPageVM()
        {
            Master = Application.Current.Resources["User"] as User;
            Date_Time = DateTime.Now;
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


        private DelegateCommand _addBookingCommand;
        public ICommand AddBookingCommand
        {
            get
            {
                if (_addBookingCommand == null)
                    _addBookingCommand = new DelegateCommand(AddBooking);
                return _addBookingCommand;
            }
        }

        private void AddBooking()
        {
            Booking booking = new Booking(Master.Id, Date_Time, Service);
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
            }
            homePageMasterFrame = Application.Current.Resources["HomePageMasterFrame"] as Frame;
            homePageMasterFrame.Navigate(new AsMasterPage());
        }
    }
}
