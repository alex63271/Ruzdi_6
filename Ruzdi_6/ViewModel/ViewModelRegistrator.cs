using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.ViewModel
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
           .AddSingleton<VM_ForGlavnaya>()
           .AddSingleton<VM_Applicant>()
           .AddSingleton<VM_Contract>()
           .AddSingleton<VM_For_Win_UP1>()
           .AddSingleton<VM_Pledgor>()
           .AddSingleton<VM_Pledgee>()
           .AddSingleton<VM_Property>()
           //.AddSingleton<VM_SettingsDB>()
           .AddSingleton<VMWindowForUZ1>()
           ;
    }
}
