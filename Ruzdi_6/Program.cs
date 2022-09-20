using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ruzdi_6.Data;
using Ruzdi_6.Services;
using Ruzdi_6.ViewModel;

namespace Ruzdi_6
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args)        
            => Host.CreateDefaultBuilder(args)
                //.ConfigureServices(App.ConfigureServices)
                .ConfigureServices(
                                    (HostBuilderContext host, IServiceCollection services) => services
                                                                                              .AddDatabase(host.Configuration.GetSection("Database")) // секция из appsettings.json
                                                                                              .AddServices()
                                                                                              .AddViewModels()                                                                                                
                                                                                              );                                                                                                    
    }
}
