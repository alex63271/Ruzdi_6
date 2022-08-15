using Microsoft.Extensions.DependencyInjection;
using Ruzdi_DB.Entityes;
using Ruzdi_Interfaces;

namespace Ruzdi_DB
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<Contracts>, ContractsRepository>()
            .AddTransient<IRepository<Notification>, NotificationRepository>()
            .AddTransient<IRepository<Organizations>, DbRepository<Organizations>>()
            .AddTransient<IRepository<Persons>, DbRepository<Persons>>()
            .AddTransient<IRepository<Regions>, DbRepository<Regions>>()
            ;
    }
}
