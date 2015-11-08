using System;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using RxApplication.ViewModels;
using RxDemoCode.Interfaces;

namespace RxApplication.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MainViewModel ViewModel { get { return this.DataContext as MainViewModel; } }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Action<string> callback = x => ViewModel.TextResult = x + Environment.NewLine + ViewModel.TextResult;

            var d = ServiceLocator.Current.GetInstance<IDemo1Service>();
            d.Demo4Setup(this, callback);
        }
    }
}
