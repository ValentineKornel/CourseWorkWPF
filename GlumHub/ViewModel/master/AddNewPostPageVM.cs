using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GlumHub
{
    class AddNewPostPageVM : INotifyPropertyChanged
    {

        private User _user;

        byte[] _postImage;
        public byte[] PostImage
        {
            get { return _postImage; }
            set
            {
                _postImage = value;
                OnPropertyChanged(nameof(PostImage));
            }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        public AddNewPostPageVM()
        {
            _user = Application.Current.Resources["User"] as User;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private DelegateCommand _loadImageCommand;  
        public ICommand LoadImageCommand
        {
            get
            {
                if (_loadImageCommand == null)
                    _loadImageCommand = new DelegateCommand(LoadImage);
                return _loadImageCommand;
            }
        }

        public void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    byte[] imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                    PostImage = imageData;
                }
                Image img = Application.Current.Resources["PostImage"] as Image;
                img.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }


        private DelegateCommand _addNewPostCommand;
        public ICommand AddNewPostCommand
        {
            get
            {
                if (_addNewPostCommand == null)
                    _addNewPostCommand = new DelegateCommand(AddNewPost);
                return _addNewPostCommand;
            }
        }

        public void AddNewPost()
        {

            Post post = new Post(_user.Id, PostImage, Description);

            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }

            Frame myProfilePageMasterFrame = Application.Current.Resources["MyProfilePageMasterFrame"] as Frame;
            myProfilePageMasterFrame.Navigate(new PostsPage());
        }
    }
}

