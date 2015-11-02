using System;
using System.Threading;
using GalaSoft.MvvmLight;


namespace RxApplication.ViewModels
{
    public class PresentationViewModel : ViewModelBase
    {
        public PresentationViewModel()
        {

        }

        public string PresentationText { get; set; }  
    }
}