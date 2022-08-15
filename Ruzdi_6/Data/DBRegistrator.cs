using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Ruzdi_DB.Context;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruzdi_DB;

namespace Ruzdi_6.Data
{
   public static class DBRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<DB_Ruzdi>(opt => opt.UseMySql(configuration.GetConnectionString("MySql"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySql")) ))
            .AddRepositoriesInDB()
            .AddTransient<DbInizialaizer>()
            ;
    }
}
