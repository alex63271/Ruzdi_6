using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Context;
using System.Windows;

namespace Ruzdi_6.Data
{
    public static class DBRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<DB_Ruzdi>(opt =>
            {

                try
                {
                    opt.UseMySql(configuration.GetConnectionString("MySql"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySql")));
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Unable to connect to any of the specified MySQL hosts"))
                    {
                        MessageBox.Show("Не удалось подключиться к БД Mysql");
                    }
                    return;
                }
            }, ServiceLifetime.Transient)
            ;
    }
}
