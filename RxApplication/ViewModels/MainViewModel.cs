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
            Demo0Command = new RelayCommand(DoDemo0);
            Demo1Command = new RelayCommand(DoDemo1);
            Demo2Command = new RelayCommand(DoDemo2);
            Demo2_2Command = new RelayCommand(DoDemo2_2);
            Demo2_3Command = new RelayCommand(DoDemo2_3);
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

        public RelayCommand Demo0Command { get; set; }
        public RelayCommand Demo1Command { get; set; }
        public RelayCommand Demo2Command { get; set; }
        public RelayCommand Demo2_2Command { get; set; }
        public RelayCommand Demo2_3Command { get; set; }
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
            pVm.ShowPresentationText(pageText);
        }

        private void DoDemo0()
        {
            ShowPresentationPage("Demo 0 shows...");

            _demo1Svc.Demo0(_callback);
        }

        private void DoDemo1()
        {
            ShowPresentationPage("Demo 1 shows...");

            _demo1Svc.Demo1(_callback);
        }

        private void DoDemo2_2()
        {
            ShowPresentationPage("Demo 2_2 shows...");

            _demo1Svc.Demo2_2(_callback);
        }

        private void DoDemo2_3()
        {
            ShowPresentationPage("Demo 2_3 shows...");

            _demo1Svc.Demo2_3(_callback);
        }

        private void DoDemo2()
        {
            ShowPresentationPage("Demo 2 shows...");

            _demo1Svc.Demo2(_callback);
        }

        private void DoDemo3()
        {
            ShowPresentationPage("Demo 3 shows...");
            _demo1Svc.Demo3(_callback);
        }

        private void ToggleDemo4()
        {
            ShowPresentationPage("Demo 4 shows...");
            _demo1Svc.Demo4Toggle();
        }

        private void DoDemo5()
        {
            ShowPresentationPage("Demo 5 shows...");
            _demo1Svc.Demo5(_callback);
        }

        private void DoDemo6()
        {
            ShowPresentationPage("Demo 6 shows...");
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