using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using RxApplication.ViewModels;

namespace RxApplication.Locators
{
    public class VmLocator
    {
        public VmLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<GraphViewModel>();
            SimpleIoc.Default.Register<PresentationViewModel>();
        }

        public MainViewModel Main { get { return ServiceLocator.Current.GetInstance<MainViewModel>(); } }
        public GraphViewModel Graph { get { return ServiceLocator.Current.GetInstance<GraphViewModel>(); } }
        public PresentationViewModel Presentation { get { return ServiceLocator.Current.GetInstance<PresentationViewModel>(); } }
    }
}