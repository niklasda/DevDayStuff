using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Practices.ServiceLocation;
using RxApplication.Views;
using RxDemoCode.Interfaces;

namespace RxApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Demo1Command = new RelayCommand(DoDemo1);
            Demo2Command = new RelayCommand(DoDemo2);
            Demo3Command = new RelayCommand(DoDemo3);
            GraphCommand = new RelayCommand(DoGraphDemo);
            PresentationCommand = new RelayCommand(OpenPresentationWindow);

            _demo1Svc = ServiceLocator.Current.GetInstance<IDemo1Service>();

        }

        private IDemo1Service _demo1Svc;
        private string _textResult;
        public string TextResult
        {
            get { return _textResult; }
            set
            {
                if (_textResult != value)
                {
                    _textResult = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand MouseMove { get; set; }


        public RelayCommand Demo1Command { get; set; }
        public RelayCommand Demo2Command { get; set; }
        public RelayCommand Demo3Command { get; set; }
        public RelayCommand GraphCommand { get; set; }
        public RelayCommand PresentationCommand { get; set; }

        private void DoDemo1()
        {
            Action<long> callback = x => TextResult += x.ToString();
            _demo1Svc.Demo1(callback);
        }

        private void DoDemo2()
        {
            Action<string> callback = x => TextResult += x;
            _demo1Svc.Demo2(callback);
        }

        private void DoDemo3()
        {
            Action<double> callback = x => TextResult += x;
            _demo1Svc.Demo3(callback);
        }

        private void DoGraphDemo()
        {
            var graphWindow = new GraphWindow();
            graphWindow.ShowDialog();
        }

        private void OpenPresentationWindow()
        {
            var presentationWindow = new PresentationWindow();
            presentationWindow.ShowDialog();
        }
    }
}