using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RxDemoCode;

namespace RxApplication.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Demo1Command = new RelayCommand(SaveUser);
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

        private async void SaveUser()
        {
            var d = new Demos();
            d.Demo1(x =>
            {
                TextResult += x.ToString();
                return "";
            });
        }
    }
}