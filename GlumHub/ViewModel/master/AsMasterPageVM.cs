using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GlumHub
{
    class AsMasterPageVM
    {
        Frame asMasterPageFrame;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private DelegateCommand _upcomingBookingPageRedirectCommand;
        public ICommand UpcomingBookingPageRedirectCommand
        {
            get
            {
                if (_upcomingBookingPageRedirectCommand == null)
                    _upcomingBookingPageRedirectCommand = new DelegateCommand(UpcomingBookingPageRedirect);
                return _upcomingBookingPageRedirectCommand;
            }
        }

        private void UpcomingBookingPageRedirect()
        {
            asMasterPageFrame = Application.Current.Resources["AsMasterPageFrame"] as Frame;
            asMasterPageFrame.Navigate(new UpcomingBookingPage());
        }

        private DelegateCommand _freeBookingPageRedirectCommand;
        public ICommand FreeBookingPageRedirectCommand
        {
            get
            {
                if (_freeBookingPageRedirectCommand == null)
                    _freeBookingPageRedirectCommand = new DelegateCommand(FreeBookingPageRedirect);
                return _freeBookingPageRedirectCommand;
            }
        }

        private void FreeBookingPageRedirect()
        {
            asMasterPageFrame = Application.Current.Resources["AsMasterPageFrame"] as Frame;
            asMasterPageFrame.Navigate(new FreeBookingPage());
        }
    }
}
