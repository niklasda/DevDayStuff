using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using RxDemoCode.Interfaces;
using RxDemoCode.Services;

namespace RxApplication.Locators
{
    public class SvcLocator
    {
        public SvcLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SimpleIoc.Default.Register<IDemo1Service, Demo1Service>();
        }
    }
}