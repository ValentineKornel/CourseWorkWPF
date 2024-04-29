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
using static GlumHub.SearchPageVM;

namespace GlumHub
{
    internal class MyMastersPageVM
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

        private ObservableCollection<UserWrapper> _myMasters;
        public ObservableCollection<UserWrapper> MyMasters
        {
            get { return _myMasters; }
            set
            {
                _myMasters = value;
                OnPropertyChanged(nameof(MyMasters));
            }
        }

        public MyMastersPageVM()
        {
            _myMasters = new ObservableCollection<UserWrapper>();
            _user = Application.Current.Resources["User"] as User;


            using (ApplicationContextDB db = new ApplicationContextDB())
            {

                foreach (UserRelation relation in db.UserRelations.Where(r => r.Follower.Id == _user.Id).Include(r => r.Master).Include(r => r.Master.MasterInfo).OrderBy(r => r.Master.Secondname))
                {
                    MyMasters.Add(new UserWrapper(relation.Master, MasterPageRedirectCommand));
                }

                
            }
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
                MyMasters.Clear();


                foreach (UserRelation relation in db.UserRelations.Where(r => r.Follower.Id == _user.Id && 
                (r.Master.Username.Contains(SearchText) || r.Master.Firstname.Contains(SearchText) || r.Master.Secondname.Contains(SearchText)))
                    .Include(r => r.Master).Include(r => r.Master.MasterInfo).OrderBy(r => r.Master.Secondname))
                {
                    MyMasters.Add(new UserWrapper(relation.Master, MasterPageRedirectCommand));
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
