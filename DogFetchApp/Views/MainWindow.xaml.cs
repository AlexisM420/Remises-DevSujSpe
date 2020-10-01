using DogFetchApp.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel currentViewmodel;
        
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.ApiHelper.InitializeClient();

            currentViewmodel = new MainViewModel();

            DataContext = currentViewmodel;

            Task.Run(() => currentViewmodel.LoadBreeds());
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //await currentViewmodel.LoadBreeds();
            /*myCbox.ItemsSource = currentViewmodel.Breeds;
            myCbox.SelectedItem = currentViewmodel.SelectedBreed;

            myCbox2.SelectedItem = currentViewmodel.SelectedAmount;*/
            /*SelectedItem = "{Binding SomeBindingPropertySelectedCurrently, 
                    Mode = TwoWay, UpdateSourceTrigger = PropertyChanged}"*/
        }
    }
}
