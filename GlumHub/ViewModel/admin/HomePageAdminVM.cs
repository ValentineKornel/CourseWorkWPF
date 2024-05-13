using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
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
    internal class HomePageAdminVM : INotifyPropertyChanged
    {
        Frame homePageFrame;



        private ObservableCollection<RequestWrapper> _requests;
        public ObservableCollection<RequestWrapper> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }

        public HomePageAdminVM()
        {
            Requests = new ObservableCollection<RequestWrapper>();
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                
                foreach(Request request in db.Requests.Include(r => r.Client))
                {
                    Requests.Add(new RequestWrapper(request, RequestPageRedirectCommand));
                }

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand<long?> _requestPageRedirectCommand;
        public ICommand RequestPageRedirectCommand
        {
            get
            {
                if (_requestPageRedirectCommand == null)
                    _requestPageRedirectCommand = new DelegateCommand<long?>(RequestPageRedirect);
                return _requestPageRedirectCommand;
            }
        }

        private void RequestPageRedirect(long? Requestd)
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            if (Application.Current.Resources["ReuestId"] == null)
                Application.Current.Resources.Add("ReuestId", Requestd);
            else
                Application.Current.Resources["ReuestId"] = Requestd;
            homePageFrame.Navigate(new RequestPage());
        }



        public class RequestWrapper
        {
            public Request request { get; set; }
            public ICommand UserPageRedirectCommand { get; set; }

            public RequestWrapper(Request request, ICommand userPageRedirectCommand)
            {
                this.request = request;
                UserPageRedirectCommand = userPageRedirectCommand;
            }
        }


        
    }
}
