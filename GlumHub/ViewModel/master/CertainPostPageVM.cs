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
    internal class CertainPostPageVM : INotifyPropertyChanged
    {

        private User _user;

        private Post _post;
        public Post post
        {
            get { return _post; }
            set
            {
                _post = value;
                OnPropertyChanged(nameof(Post));
            }
        }

        private long? PostId;

        private string _deleteButttonVisibilitiProperty;
        public string DeleteButtonVisibilityProperty
        {
            get { return _deleteButttonVisibilitiProperty; }
            set
            {
                _deleteButttonVisibilitiProperty = value;
                OnPropertyChanged(nameof(DeleteButtonVisibilityProperty));
            }
        }

        public CertainPostPageVM()
        {
            _user = Application.Current.Resources["User"] as User;

            PostId = Application.Current.Resources["PostId"] as long?;

            using (ApplicationContextDB db = new ApplicationContextDB())
            {

                post =  db.Posts.FirstOrDefault(p => p.Id == PostId);
            }

            switch (_user.Role)
            {
                case ROLES.MASTER:
                    {
                        DeleteButtonVisibilityProperty = "Visible";
                        break;
                    }
                case ROLES.CLIENT:
                    {
                        DeleteButtonVisibilityProperty = "Hidden";
                        break;
                    }
                case ROLES.ADMIN:
                    {
                        DeleteButtonVisibilityProperty = "Visible";
                        break;
                    }
                default: break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private DelegateCommand _deletePostCommand;
        public ICommand DeletePostCommand
        {
            get
            {
                if (_deletePostCommand == null)
                    _deletePostCommand = new DelegateCommand(DeletePost);
                return _deletePostCommand;
            }
        }

        public void DeletePost()
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                post = db.Posts.FirstOrDefault(p => p.Id == PostId);
                db.Posts.Remove(post);
                db.SaveChanges();
            }

            if(_user.Role == ROLES.ADMIN)
            {
                Notification.SendNotification(post.Masterid, "Ваш пост '" + post.Description + "' был удален");
                Frame userpageForAdminFrame = Application.Current.Resources["UserPageForAdminFrame"] as Frame;
                userpageForAdminFrame.Navigate(new PostsPage());
            }
            else
            {
                Frame myProfilePageMasterFrame = Application.Current.Resources["MyProfilePageMasterFrame"] as Frame;
                myProfilePageMasterFrame.Navigate(new PostsPage());
            }
        }
    }
}
