using System.Windows;
using RxApplication.Locators;

namespace RxApplication
{
    public partial class App : Application
    {
        public App()
        {
            InitLocators();
        }

        private static void InitLocators()
        {
            var svcLoc = new SvcLocator();
            var vmLoc = new VmLocator();
        }
    }
}
