﻿using Prism.Commands;
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
    class SettigsPageVM : INotifyPropertyChanged
    {
        private ResourceDictionary englishDictionary;
        private ResourceDictionary russianDictionary;
        private ResourceDictionary lightAppearanceDictionary;
        private ResourceDictionary darkAppearanceDictionary;


        public SettigsPageVM()
        {
            englishDictionary = new ResourceDictionary();
            englishDictionary.Source = new Uri("files/EnglishDictionary.xaml", UriKind.Relative);
            russianDictionary = new ResourceDictionary();
            russianDictionary.Source = new Uri("files/RussianDictionary.xaml", UriKind.Relative);
            lightAppearanceDictionary = new ResourceDictionary();
            lightAppearanceDictionary.Source = new Uri("files/LightAppearanceDictionary.xaml", UriKind.Relative);
            darkAppearanceDictionary = new ResourceDictionary();
            darkAppearanceDictionary.Source = new Uri("files/DarkAppearanceDictionary.xaml", UriKind.Relative);


        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DelegateCommand _switchAppearanceCommand;
        public ICommand SwitchAppearanceCommand
        {
            get
            {
                if (_switchAppearanceCommand == null)
                    _switchAppearanceCommand = new DelegateCommand(SwitchAppearance);
                return _switchAppearanceCommand;
            }
        }

        private void SwitchAppearance()
        {
            if (Application.Current.Resources["Appearance"] == "Light")
            {
                Application.Current.Resources.MergedDictionaries.Remove(lightAppearanceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(darkAppearanceDictionary);
                Application.Current.Resources["Appearance"] = "Dark";
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Remove(darkAppearanceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(lightAppearanceDictionary);
                Application.Current.Resources["Appearance"] = "Light";
            }
        }


        private DelegateCommand _switchLanguageCommand;
        public ICommand SwitchLanguageCommand
        {
            get
            {
                if (_switchLanguageCommand == null)
                    _switchLanguageCommand = new DelegateCommand(SwitchLanguage);
                return _switchLanguageCommand;
            }
        }

        private void SwitchLanguage()
        {
            //Application.Current.Resources.MergedDictionaries.Clear();
            if (Application.Current.Resources["Language"] == "En")
            {
                Application.Current.Resources.MergedDictionaries.Remove(englishDictionary);
                Application.Current.Resources.MergedDictionaries.Add(russianDictionary);
                Application.Current.Resources["Language"] = "Ru";
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Remove(russianDictionary);
                Application.Current.Resources.MergedDictionaries.Add(englishDictionary);
                Application.Current.Resources["Language"] = "En";
            }

        }
    }
}
