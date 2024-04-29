using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections;
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

namespace GlumHub
{
    class SearchPageVM
    {
        Frame homePageFrame;
        private User _user;

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Search();

            }
        }


        private string _searchCity;
        public string SearchCity
        {
            get { return _searchCity; }
            set
            {
                _searchCity = value;
                OnPropertyChanged(nameof(SearchCity));
                Search();
            }
        }

        private ObservableCollection<MasterWrapper> _searchedMasters;
        public ObservableCollection<MasterWrapper> SearchedMasters
        {
            get { return _searchedMasters; }
            set
            {
                _searchedMasters = value;
                OnPropertyChanged(nameof(SearchedMasters));
            }
        }

        public SearchPageVM()
        {
            _searchedMasters = new ObservableCollection<MasterWrapper>();
            _user = Application.Current.Resources["User"] as User;
            SearchCity = "";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new DelegateCommand(Search);
                return _searchCommand;
            }
        }

        private void Search()
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                SearchedMasters.Clear();

                ObservableCollection<MasterWrapper> temp = new ObservableCollection<MasterWrapper>();

                _user = db.Users.Include(u => u.MasterInfo).Include(u => u.Masters).FirstOrDefault(u => u.Id == _user.Id);

                foreach (User master in db.Users.Include(u => u.MasterInfo).Where(
                    u => (u.Username.Contains(SearchText) || u.Firstname.Contains(SearchText) || u.Secondname.Contains(SearchText))
                    && u.MasterInfo != null && u.Role == ROLES.MASTER && u.Id != _user.Id))
                {

                    
                    //UserRelation relation = db.UserRelations.Include(r => r.Master).Include(u => u.Follower).FirstOrDefault(r => r.MasterId == master.Id && r.FollowerId == _user.Id);
                    if (_user.Masters.FirstOrDefault(r => r.MasterId == master.Id && r.FollowerId == _user.Id) != null)
                    {
                        temp.Add(new MasterWrapper(master, MasterPageRedirectCommand, "Visible"));
                    }
                    else
                    {
                        temp.Add(new MasterWrapper(master, MasterPageRedirectCommand, "Hidden"));
                    }
                }
                foreach (MasterWrapper master in temp.Where(master => master.master.MasterInfo.BusinessAddress.ToLower().Contains(SearchCity.ToLower())))
                {
                    SearchedMasters.Add(master);
                }

            }
        }


        private void SearchAsAdmin()
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                SearchedMasters.Clear();


                _user = db.Users.Include(u => u.MasterInfo).Include(u => u.Masters).FirstOrDefault(u => u.Id == _user.Id);

                foreach (User master in db.Users.Include(u => u.MasterInfo).Where(
                    u => (u.Username.Contains(SearchText) || u.Firstname.Contains(SearchText) || u.Secondname.Contains(SearchText))
                    && u.Id != _user.Id))
                {
                    SearchedMasters.Add(new MasterWrapper(master, MasterPageRedirectCommand, "Hidden"));
                }
            }
        }

        private DelegateCommand<long?> _masterPageRedirectCommand;
        public ICommand MasterPageRedirectCommand
        {
            get
            {
                if (_masterPageRedirectCommand == null)
                    _masterPageRedirectCommand = new DelegateCommand<long?>(MasterPageRedirect);
                return _masterPageRedirectCommand;
            }
        }

        private void MasterPageRedirect(long? MasterId)
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            if (Application.Current.Resources["MasterId"] == null)
                Application.Current.Resources.Add("MasterId", MasterId);
            else
                Application.Current.Resources["MasterId"] = MasterId;
            homePageFrame.Navigate(new MasterPage());
        }



        public class MasterWrapper
        {
            public User master { get; set; }
            public string SubscribeTickVisibility { get; set; }
            public ICommand MasterPageRedirectCommand { get; set; }

            public MasterWrapper(User user, ICommand masterPageRedirectCommand, string subscribeTickVisibility)
            {
                this.master = user;
                MasterPageRedirectCommand = masterPageRedirectCommand;
                this.SubscribeTickVisibility = subscribeTickVisibility;
            }
        }
    }
}
