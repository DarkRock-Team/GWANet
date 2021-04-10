using GWANet;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TitleHelper.Services;
using TitleHelper.ViewModels;

namespace TitleHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public new static App Current 
            => (App)Application.Current;
        public MainWindowViewModel MainWindowVM 
            => Services.GetService<MainWindowViewModel>();

        public App()
        {
            Services = ConfigureServices();
        }
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IGWANetService, GWANetService>();
            services.AddSingleton<IGWANet, GWANet.GWANet>();
            services.AddSingleton<MainWindowViewModel>();
            return services.BuildServiceProvider();
        }

        public void AppStart(object sender, StartupEventArgs args)
        {
            var mainWindow = new MainWindow();
            mainWindow?.Show();
        }

    }
}
