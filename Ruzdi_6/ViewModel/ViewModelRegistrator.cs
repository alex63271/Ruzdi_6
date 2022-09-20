using Microsoft.Extensions.DependencyInjection;

namespace Ruzdi_6.ViewModel
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services

           .AddScoped<VM_ForGlavnaya>()

           .AddTransient<VMWindowForUZ1>()

           .AddScoped<VM_For_Win_UP1>()


           .AddScoped<VM_Pledgor>()
           .AddScoped<VM_Pledgee>()
           .AddScoped<VM_Property>()          
           .AddScoped<VM_Contract>()
           .AddScoped<VM_Applicant>()


           .AddTransient<VM_Settings>()
           
           ;
    }
}
