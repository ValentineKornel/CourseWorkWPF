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
    internal class CertainPostPageVM
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

        private string _editButttonVisibilitiProperty;
        public string EditButtonVisibilityProperty
        {
            get { return _editButttonVisibilitiProperty; }
            set
            {
                _editButttonVisibilitiProperty = value;
                OnPropertyChanged(nameof(EditButtonVisibilityProperty));
            }
        }

        public CertainPostPageVM()
        {
            _user = Application.Current.Resources["User"] as User;

            long? PostId = Application.Current.Resources["PostId"] as long?;

            using (ApplicationContextDB db = new ApplicationContextDB())
            {

                post =  db.Posts.FirstOrDefault(p => p.Id == PostId);
            }

            switch (_user.Role)
            {
                case ROLES.MASTER:
                    {
                        DeleteButtonVisibilityProperty = "Visible";
                        EditButtonVisibilityProperty = "Visible";
                        break;
                    }
                case ROLES.CLIENT:
                    {
                        DeleteButtonVisibilityProperty = "Hidden";
                        EditButtonVisibilityProperty = "Hidden";
                        break;
                    }
                case ROLES.ADMIN:
                    {
                        DeleteButtonVisibilityProperty = "Visible";
                        EditButtonVisibilityProperty = "Hidden";
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
            MessageBox.Show("Deleting post");
        }

        private DelegateCommand _editPostRedirectCommand;
        public ICommand EditPostRedirectCommand
        {
            get
            {
                if (_editPostRedirectCommand == null)
                    _editPostRedirectCommand = new DelegateCommand(DeletePost);
                return _editPostRedirectCommand;
            }
        }

        public void EditPostRedirect()
        {
            MessageBox.Show("Edit post redirecting");
        }
    }
}
