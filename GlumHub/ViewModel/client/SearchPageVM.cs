using Prism.Commands;
using System;
using System.Collections;
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

        private ObservableCollection<UserWrapper> _searchedMasters;
        public ObservableCollection<UserWrapper> SearchedMasters
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
            _searchedMasters = new ObservableCollection<UserWrapper>();
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

                ObservableCollection<UserWrapper> temp = new ObservableCollection<UserWrapper>();

                foreach (User u in db.Users.Where(
                    u => (u.Username.Contains(SearchText) || u.Firstname.Contains(SearchText) || u.Secondname.Contains(SearchText))
                    && u.MasterInfo != null && u.Role == ROLES.MASTER && u.Id != _user.Id))
                {
                    temp.Add(new UserWrapper(u, MasterPageRedirectCommand));
                }
                foreach (UserWrapper u in temp)
                {
                    u.user.MasterInfo = db.MasterInfos.FirstOrDefault(i => i.UserId == u.user.Id);
                }
                foreach (UserWrapper u in temp.Where(u => u.user.MasterInfo.BusinessAddress.ToLower().Contains(SearchCity.ToLower())))
                {
                    SearchedMasters.Add(u);
                }
                //SearchedMasters = new ObservableCollection<UserWrapper>(SearchedMasters.Where(i => i.user.MasterInfo.BusinessAddress.Contains(SearchCity)));

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



        public class UserWrapper
        {
            public User user { get; set; }
            public ICommand MasterPageRedirectCommand { get; set; }

            public UserWrapper(User user, ICommand masterPageRedirectCommand)
            {
                this.user = user;
                MasterPageRedirectCommand = masterPageRedirectCommand;
            }
        }
    }
}
