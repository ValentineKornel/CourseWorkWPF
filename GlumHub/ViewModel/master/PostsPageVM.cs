using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static GlumHub.SearchPageVM;

namespace GlumHub
{
    class PostsPageVM : INotifyPropertyChanged
    {

        Frame myProfilePageMasterFrame;

        private User User;

        private string _AddNewPostButtonVisibility;
        public string AddNewPostButtonVisibility
        {
            get { return _AddNewPostButtonVisibility; }
            set
            {
                _AddNewPostButtonVisibility = value;
                OnPropertyChanged(nameof(AddNewPostButtonVisibility));
            }
        }

        private ObservableCollection<PostWrapper> _posts;
        public ObservableCollection<PostWrapper> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                OnPropertyChanged(nameof(Posts));
            }
        }

        public PostsPageVM()
        {
            User = Application.Current.Resources["User"] as User;
            myProfilePageMasterFrame = Application.Current.Resources["MyProfilePageMasterFrame"] as Frame;

            if(User.Role == ROLES.MASTER)
            {
                AddNewPostButtonVisibility = "Visible";
            }
            else
            {
                AddNewPostButtonVisibility = "Hidden";
            }

            Posts = new ObservableCollection<PostWrapper>();
            long? MasterId = Application.Current.Resources["MasterId"] as long?;
            using (ApplicationContextDB db = new ApplicationContextDB())
            {

                foreach(Post post in db.Posts.Where(p => p.Masterid == MasterId))
                {
                    Posts.Add(new PostWrapper(post, PostPageRedirectCommand));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DelegateCommand _addNewPostRedirectCommand;
        public ICommand AddNewPostRedirectCommand
        {
            get
            {
                if (_addNewPostRedirectCommand == null)
                    _addNewPostRedirectCommand = new DelegateCommand(AddNewPostRedirect);
                return _addNewPostRedirectCommand;
            }
        }

        private void AddNewPostRedirect()
        {
            myProfilePageMasterFrame.Navigate(new AddNewPostPage());

        }


        private DelegateCommand<long?> _postPageRedirectCommand;
        public ICommand PostPageRedirectCommand
        {
            get
            {
                if (_postPageRedirectCommand == null)
                    _postPageRedirectCommand = new DelegateCommand<long?>(PostRedirect);
                return _postPageRedirectCommand;
            }
        }

        private void PostRedirect(long? PostId)
        {
            if (Application.Current.Resources["PostId"] == null)
            {
                Application.Current.Resources.Add("PostId", PostId);
            }
            else
            {
                Application.Current.Resources["PostId"] = PostId;
            }

            Frame masterPageForClientFrame = Application.Current.Resources["MasterPageForClientFrame"] as Frame;
            Frame userpageForAdminFrame = Application.Current.Resources["UserPageForAdminFrame"] as Frame;


            switch (User.Role)
            {
                case ROLES.MASTER:
                    {
                        myProfilePageMasterFrame.Navigate(new CertainPostPage());
                        break;
                    }
                case ROLES.CLIENT:
                    {
                        masterPageForClientFrame.Navigate(new CertainPostPage());
                        break;
                    }
                case ROLES.ADMIN:
                    {
                        userpageForAdminFrame.Navigate(new CertainPostPage());
                        break;
                    }
                default: break;
            }

        }

        public class PostWrapper
        {
            public Post post { get; set; }
            public ICommand PostPageRedirectCommand { get; set; }

            public PostWrapper(Post post, ICommand PostPageRedirectCommand)
            {
                this.post = post;
                this.PostPageRedirectCommand = PostPageRedirectCommand;
            }
        }
    }
}
