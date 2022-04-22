using Microsoft.Extensions.Hosting;
using PortraitsDrawing.Services;

using PortraitsDrawing.Views;

using System.Windows;

namespace PortraitsDrawing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                        .ConfigureServices(services => services.CreateConfiguration())
                        .Build();

            IocControl.Initialize(_host.Services);
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
        }
    }
}
