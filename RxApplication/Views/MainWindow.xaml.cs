using System.Windows;
using Microsoft.Practices.ServiceLocation;
using RxDemoCode.Interfaces;

namespace RxApplication.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var d = ServiceLocator.Current.GetInstance<IDemo1Service>();
            //var d = new DemoPart1();
            d.Demo4(this);
        }
    }
}
