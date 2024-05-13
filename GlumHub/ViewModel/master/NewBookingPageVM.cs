using GlumHub.views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GlumHub
{
    class NewBookingPageVM : INotifyPropertyChanged
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
            if (ValidateAll())
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


        private bool validateService()
        {
            if (string.IsNullOrWhiteSpace(Service))
            {
                new MyMessageBox("Услуга не может быть пустой").Show();
                return false;
            }
            return true;
        }

        private bool validateDate_time()
        {
            if (Date_Time <= DateTime.Now)
            {
                new MyMessageBox("Неверный формат времени").Show();
                return false;
            }
            return true;
        }

        private bool ValidateAll()
        {
            return validateService() && validateDate_time();
        }
    }
}
