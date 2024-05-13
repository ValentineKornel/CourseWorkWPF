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
    class HomePageMasterVM : INotifyPropertyChanged
    {
        Frame homePageMasterFrmae;




        public HomePageMasterVM()
        {
            User master = Application.Current.Resources["User"] as User;
            if (Application.Current.Resources["MasterId"] == null)
                Application.Current.Resources.Add("MasterId", master.Id);
            else
                Application.Current.Resources["MasterId"] = master.Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private DelegateCommand _asMasterPageRedirectCommand;
        public ICommand AsMasterPageRedirectCommand
        {
            get
            {
                if (_asMasterPageRedirectCommand == null)
                    _asMasterPageRedirectCommand = new DelegateCommand(AsMasterPageRedirect);
                return _asMasterPageRedirectCommand;
            }
        }

        private void AsMasterPageRedirect()
        {
            homePageMasterFrmae = Application.Current.Resources["HomePageMasterFrame"] as Frame;
            homePageMasterFrmae.Navigate(new AsMasterPage());
        }

        private DelegateCommand _asClientPageRedirectCommand;
        public ICommand AsClientPageRedirectCommand
        {
            get
            {
                if (_asClientPageRedirectCommand == null)
                    _asClientPageRedirectCommand = new DelegateCommand(AsClientPageRedirect);
                return _asClientPageRedirectCommand;
            }
        }

        private void AsClientPageRedirect()
        {
            homePageMasterFrmae = Application.Current.Resources["HomePageMasterFrame"] as Frame;
            homePageMasterFrmae.Navigate(new HomePageClient());
        }
    }
}
