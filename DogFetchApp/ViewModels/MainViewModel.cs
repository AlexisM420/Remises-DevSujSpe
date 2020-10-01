using ApiHelper;
using ApiHelper.Models;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public DelegateCommand<string> ChangeLanguageCommand { get; private set; }
        public AsyncCommand<string> UserFetchCommand { get; private set; }

        public List<int> Quantities { get; set; } = new List<int> { 1, 3, 5, 10 };

        private ObservableCollection<string> breeds;
        public ObservableCollection<string> Breeds
        {
            get => breeds;
            set
            {
                breeds = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> pics;
        public ObservableCollection<string> Pics
        {
            get => pics;
            set
            {
                pics = value;
                OnPropertyChanged();
            }
        }

        private string selectedBreed;
        public string SelectedBreed
        {
            get => selectedBreed;
            set
            {
                selectedBreed = value;
                OnPropertyChanged();
            }
        }

        private int selectedAmount;
        public int SelectedAmount
        {
            get => selectedAmount;
            set
            {
                selectedAmount = value;
                OnPropertyChanged();
            }
        }
        //public AsyncCommand<object> WindowLoadedCommand { get; private set; }
        //public AsyncCommand<int> LoadBreedsCommand { get; private set; }

        public MainViewModel()
        {
            ChangeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
            UserFetchCommand = new AsyncCommand<string>(LoadImages);
            Breeds = new ObservableCollection<string>();
            Pics = new ObservableCollection<string>();

            //LoadBreedsCommand = new AsyncCommand<int>(LoadBreeds);
            //WindowLoadedCommand = new AsyncCommand<object>(WindowLoaded);
        }
        private void ChangeLanguage(string param)
        {
            Properties.Settings.Default.Language = param;
            Properties.Settings.Default.Save();

            if (MessageBox.Show(
                    "Please restart app for the settings to take effect.\nWould you like to restart?",
                    "Warning!",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Restart();
        }

        private async Task LoadImages(string param)
        {
            int cpt = 0;
            Pics.Clear();
            do
            {
                DogResultModel image = await DogApiProcessor.GetImageUrl(SelectedBreed);
                Pics.Add(image.Message);
                cpt++;
            } while (SelectedAmount > cpt);
        }
        public async Task LoadBreeds()
        {
            Breeds = await DogApiProcessor.LoadBreedsList();
            SelectedBreed = Breeds[0];
        }

        void Restart()
        {
            var filename = Application.ResourceAssembly.Location;
            var newFile = Path.ChangeExtension(filename, ".exe");
            Process.Start(newFile);
            Application.Current.Shutdown();
        }

        //private async Task WindowLoaded(object arg) => await LoadBreeds();
    }
}
