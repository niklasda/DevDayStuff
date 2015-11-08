using System;
using System.Threading;
using System.Windows;
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
            Demo4Command = new RelayCommand(ToggleDemo4);
            Demo5Command = new RelayCommand(DoDemo5);
            Demo6Command = new RelayCommand(DoDemo6);
            GraphCommand = new RelayCommand(DoGraphDemo);
            PresentationCommand = new RelayCommand(OpenPresentationWindow);
            ClearLogCommand = new RelayCommand(DoClearLog);

            _demo1Svc = ServiceLocator.Current.GetInstance<IDemo1Service>();

            _callback = AppendLineWithPidToTextResult;
        }

        private PresentationWindow _presentationWindow;

        private GraphWindow _graphWindow;

        private readonly Action<string> _callback;

        private readonly IDemo1Service _demo1Svc;

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

        //public RelayCommand MouseMove { get; set; }
        
        public RelayCommand Demo1Command { get; set; }
        public RelayCommand Demo2Command { get; set; }
        public RelayCommand Demo3Command { get; set; }
        public RelayCommand Demo4Command { get; set; }
        public RelayCommand Demo5Command { get; set; }
        public RelayCommand Demo6Command { get; set; }
        public RelayCommand GraphCommand { get; set; }
        public RelayCommand PresentationCommand { get; set; }
        public RelayCommand ClearLogCommand { get; set; }

        private void ShowPresentationPage(string pageText)
        {
            OpenPresentationWindow();

            var pVm = ServiceLocator.Current.GetInstance<PresentationViewModel>();
            pVm.PresentationText = pageText;
        }

        private void DoDemo1()
        {
            ShowPresentationPage("Demo 1 shows...");

            //  Action<long> callback = x => TextResult += x.ToString();
            _demo1Svc.Demo1(_callback);
        }

        private void DoDemo2()
        {
            ShowPresentationPage("Demo 2 shows...");

            //Action<string> callback = x => TextResult += x;
            _demo1Svc.Demo2(_callback);
        }

        private void DoDemo3()
        {
            ShowPresentationPage("Demo 3 shows...");
            //            Action<double> callback = x => TextResult += x;
            _demo1Svc.Demo3(_callback);
        }

        private void ToggleDemo4()
        {
            //            Action<double> callback = x => TextResult += x;
            _demo1Svc.Demo4Toggle();
        }

        private void DoDemo5()
        {
            _demo1Svc.Demo5(_callback);
        }

        private void DoDemo6()
        {
            //nbAction<string> callback = x => TextResult += x + Environment.NewLine;
            _demo1Svc.Demo6(_callback);
        }

        private void DoGraphDemo()
        {
            if (_graphWindow == null)
            {
                _graphWindow = new GraphWindow();
                _graphWindow.Owner = Application.Current.MainWindow;
                _graphWindow.Closed += (o, e) => _graphWindow = null;
                _graphWindow.Show();
            }
            else
            {
                _graphWindow.Activate();
            }
        }

        private void OpenPresentationWindow()
        {
            if (_presentationWindow == null)
            {
                _presentationWindow = new PresentationWindow();
                _presentationWindow.Owner = Application.Current.MainWindow;
                _presentationWindow.Closed += (o, e) => _presentationWindow = null;
                _presentationWindow.Show();
            }
            else
            {
                _presentationWindow.Activate();
            }
        }

        private void DoClearLog()
        {
            TextResult = "";
        }

        private void AppendLineWithPidToTextResult(string text)
        {
            TextResult += text;
            TextResult += " - Thread: ";
            TextResult += Thread.CurrentThread.ManagedThreadId.ToString();
            TextResult += Environment.NewLine;
        }
    }
}