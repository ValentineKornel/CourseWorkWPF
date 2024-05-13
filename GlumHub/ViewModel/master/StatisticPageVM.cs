using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GlumHub
{
    class StatisticPageVM : INotifyPropertyChanged
    {
        User User;


        private int _ervicesComplitedTotalAmount;
        public int ServicesComplitedTotalAmount
        {
            get { return _ervicesComplitedTotalAmount;}
            set 
            {  
                _ervicesComplitedTotalAmount = value;
                OnPropertyChanged(nameof(ServicesComplitedTotalAmount));
            }
        }

        private ObservableCollection<ClientWrapper> _bestClients;
        public ObservableCollection<ClientWrapper> BestClents
        {
            get { return _bestClients; }
            set
            {
                _bestClients = value;
                OnPropertyChanged(nameof(BestClents));
            }
        }

        public StatisticPageVM()
        {
            User = Application.Current.Resources["User"] as User;
            BestClents = new ObservableCollection<ClientWrapper>();


            using(ApplicationContextDB db  = new ApplicationContextDB())
            {
                ServicesComplitedTotalAmount = db.Bookings.Where(b => b.MasterId == User.Id && b.Booked == true && b.Date_Time < DateTime.Now).Count();

                var query = (from b in db.Bookings.Include(b => b.Client)
                             where b.MasterId == User.Id && b.Booked == true
                             group b by b.Client into g
                             select new ClientWrapper
                             {
                                 Client = g.Key,
                                 VisitsCount = g.Count()
                             })
                            .OrderByDescending(wrapper => wrapper.VisitsCount)
                            .Take(3);
                foreach(var client in query) {
                    BestClents.Add(client);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public class ClientWrapper
        {
            public User Client { get; set; }
            public int VisitsCount { get; set; }
        }
    }
}
