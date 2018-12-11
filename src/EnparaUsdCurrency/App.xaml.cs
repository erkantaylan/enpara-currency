using System.Windows;
using EnparaUsdCurrency.Models;
using EnparaUsdCurrency.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;
using Prism.Unity;

namespace EnparaUsdCurrency
{

    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Currency>();
            containerRegistry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }

}