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
    internal class SearchPageAdminVM
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
                //Search();
            }
        }

        private ObservableCollection<UserWrapper> _searchedUsers;
        public ObservableCollection<UserWrapper> SearchedUsers
        {
            get { return _searchedUsers; }
            set
            {
                _searchedUsers = value;
                OnPropertyChanged(nameof(SearchedUsers));
            }
        }

        public SearchPageAdminVM()
        {
            _searchedUsers = new ObservableCollection<UserWrapper>();
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
                SearchedUsers.Clear();


                _user = db.Users.Include(u => u.MasterInfo).Include(u => u.Masters).FirstOrDefault(u => u.Id == _user.Id);

                foreach (User master in db.Users.Include(u => u.MasterInfo).Where(
                    u => (u.Username.Contains(SearchText) || u.Firstname.Contains(SearchText) || u.Secondname.Contains(SearchText))
                    && u.Id != _user.Id))
                {
                    SearchedUsers.Add(new UserWrapper(master, UserPageRedirectCommand, "Hidden"));
                }
            }
            
        }

        private DelegateCommand<long?> _userPageRedirectCommand;
        public ICommand UserPageRedirectCommand
        {
            get
            {
                if (_userPageRedirectCommand == null)
                    _userPageRedirectCommand = new DelegateCommand<long?>(UserPageRedirect);
                return _userPageRedirectCommand;
            }
        }

        private void UserPageRedirect(long? UserId)
        {
            homePageFrame = Application.Current.Resources["HomePageFrame"] as Frame;
            if (Application.Current.Resources["UserId"] == null)
                Application.Current.Resources.Add("UserId", UserId);
            else
                Application.Current.Resources["UserId"] = UserId;

            if (Application.Current.Resources["MasterId"] == null)
                Application.Current.Resources.Add("MasterId", UserId);
            else
                Application.Current.Resources["MasterId"] = UserId;
            homePageFrame.Navigate(new UserPage());
        }



        public class UserWrapper
        {
            public User user { get; set; }
            public string SubscribeTickVisibility { get; set; }
            public ICommand UserPageRedirectCommand { get; set; }

            public UserWrapper(User user, ICommand userPageRedirectCommand, string subscribeTickVisibility)
            {
                this.user = user;
                UserPageRedirectCommand = userPageRedirectCommand;
                this.SubscribeTickVisibility = subscribeTickVisibility;
            }
        }
    }
}
