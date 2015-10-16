using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RxDemoCode.Demos;

namespace RxApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Demo1Command = new RelayCommand(DoDemo1);
            Demo2Command = new RelayCommand(DoDemo2);
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

        public RelayCommand Demo1Command { get; set; }

        public RelayCommand Demo2Command { get; set; }

        private void DoDemo1()
        {
            var d = new Demos();
            d.Demo1(x => TextResult += x.ToString());
        }


        private void DoDemo2()
        {
            var d = new Demos();
            d.Demo2(x => TextResult += x);
        }
    }
}