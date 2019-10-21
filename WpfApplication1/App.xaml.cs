using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using WpfApplication1.ViewModels;
using WpfApplication1.Views;

namespace WpfApplication1
{
    
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry c)
        {
            //c.RegisterSingleton()
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<ShellView, ShellViewModel>();
        }
        
        
    }
}