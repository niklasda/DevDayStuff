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
            GraphCommand = new RelayCommand(DoGraphDemo);
            PresentationCommand = new RelayCommand(OpenPresentationWindow);
        }

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
        public RelayCommand GraphCommand { get; set; }
        public RelayCommand PresentationCommand { get; set; }

        private void DoDemo1()
        {
            var d = ServiceLocator.Current.GetInstance<IDemo1Service>();
            //var d = new DemoPart1();
            d.Demo1(x => TextResult += x.ToString());
        }
        
        private void DoDemo2()
        {
            var d = ServiceLocator.Current.GetInstance<IDemo1Service>();
//            var d = new DemoPart1();
            d.Demo2(x => TextResult += x);
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