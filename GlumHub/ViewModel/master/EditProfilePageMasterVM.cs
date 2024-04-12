using Microsoft.EntityFrameworkCore;
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

namespace GlumHub
{
    class EditProfilePageMasterVM
    {
        Frame mainFrame;

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        byte[] _profileImage;
        public byte[] ProfileImage
        {
            get { return _profileImage; }
            set
            {
                _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        private string _bio;
        public string Bio
        {
            get { return _bio; }
            set
            {
                _bio = value;
                OnPropertyChanged(nameof(Bio));
            }
        }

        private string _businessAddress;
        public string BusinessAddress
        {
            get { return _businessAddress; }
            set
            {
                _businessAddress = value;
                OnPropertyChanged(nameof(BusinessAddress));
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public EditProfilePageMasterVM()
        {
            _user = Application.Current.Resources["User"] as User;
            _profileImage = User.ProfileImage;
            if(User.MasterInfo != null)
            {
                _bio = User.MasterInfo.Bio;
                _businessAddress = User.MasterInfo.BusinessAddress;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand _changeProfileImageCommand;
        public ICommand ChangeProfileImageCommand
        {
            get
            {
                if (_changeProfileImageCommand == null)
                    _changeProfileImageCommand = new DelegateCommand(ChangeProfileImage);
                return _changeProfileImageCommand;
            }
        }

        public void ChangeProfileImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    byte[] imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                    ProfileImage = imageData;
                }
            }
        }

        /*private DelegateCommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                    _saveChangesCommand = new DelegateCommand(SaveChanges);
                return _saveChangesCommand;
            }
        }*/

        private DelegateCommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                    _saveChangesCommand = new DelegateCommand(async () => await SaveChangesAsync());
                return _saveChangesCommand;
            }
        }


        public async Task SaveChangesAsync()
        {
            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                using (var transaction = await db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        User userToChange = await db.Users.FirstOrDefaultAsync(u => u.Id == User.Id);
                        if (userToChange != null)
                        {
                            userToChange.ProfileImage = ProfileImage;

                            MasterInfo masterInfo;
                            bool isThereMasterInfo = await db.MasterInfos.AnyAsync(i => i.UserId == User.Id);

                            if (isThereMasterInfo)
                            {
                                masterInfo = await db.MasterInfos.FirstOrDefaultAsync(i => i.UserId == User.Id);
                                if (masterInfo != null)
                                {
                                    masterInfo.Bio = Bio;
                                    masterInfo.BusinessAddress = BusinessAddress;
                                }
                            }
                            else
                            {
                                masterInfo = new MasterInfo(Bio, BusinessAddress, userToChange.Id);
                                db.MasterInfos.Add(masterInfo);
                            }

                            await db.SaveChangesAsync();

                            // Присваиваем обновленный MasterInfo пользователю
                            userToChange.MasterInfo = masterInfo;

                            // Сохраняем изменения
                            await db.SaveChangesAsync();

                            // Фиксируем транзакцию
                            await transaction.CommitAsync();

                            // Обновляем ресурс "User"
                            Application.Current.Resources["User"] = userToChange;

                            // Навигация на главную страницу
                            Frame mainFrame = Application.Current.Resources["MainFrame"] as Frame;
                            mainFrame.Navigate(new MainPage());
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Откатываем транзакцию в случае ошибки
                        await transaction.RollbackAsync();
                        MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
                    }
                }
            }
        }


        /*public void SaveChanges()
        {
            //TODO Devide this method to smaller methods


            using (ApplicationContextDB db = new ApplicationContextDB())
            {
                User userToChage = db.Users.FirstOrDefault(u => u.Id == User.Id);
                userToChage.ProfileImage = ProfileImage;

                MasterInfo masterInfo;
                bool isThrereMasterInfo = db.MasterInfos.Any(i => i.UserId == User.Id);
                if (isThrereMasterInfo)
                {
                    masterInfo = db.MasterInfos.FirstOrDefault(i => i.UserId == User.Id);
                    masterInfo.Bio = Bio;
                    masterInfo.BusinessAddress = BusinessAddress;
                }
                else
                {
                    masterInfo = new MasterInfo(Bio, BusinessAddress, userToChage.Id);
                    db.MasterInfos.Add(masterInfo);
                }

                db.SaveChanges();

                userToChage.MasterInfo = masterInfo;

                Application.Current.Resources["User"] = userToChage;
            }
            mainFrame = Application.Current.Resources["MainFrame"] as Frame;
            mainFrame.Navigate(new MainPage());
        }*/
    }
}

