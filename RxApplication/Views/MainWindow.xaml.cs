using System.Windows;
using RxDemoCode.Demos;

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
            var d = new DemoPart1();
            d.Demo4(this);
        }
    }
}
