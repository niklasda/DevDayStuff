using System.Windows.Markup;
using GalaSoft.MvvmLight;

namespace RxApplication.ViewModels
{
    public class PresentationViewModel : ViewModelBase
    {
        private string _presentationText;

        public string PresentationText
        {
            get { return _presentationText; }
            set
            {
                if (_presentationText != value)
                {
                    _presentationText = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void ShowPresentationText(string text)
        {
            PresentationText = text;
        }
    }
}