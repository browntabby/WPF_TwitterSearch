using Prism.Ioc;
using PriTrigger.Model;
using PriTrigger.Views;
using System.Windows;

namespace PriTrigger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ITwitterAgent, TwitterAgent>();
        }
    }
}
