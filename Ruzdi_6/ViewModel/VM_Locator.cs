using Microsoft.Extensions.DependencyInjection;

namespace Ruzdi_6.ViewModel
{
    public class VM_Locator
    {
        public VM_Locator() { }

        public static IServiceScope scopeUZ1;
        public static IServiceScope scopeUP1;

        public VM_ForGlavnaya VM_ForGlavnaya => App.Host.Services.GetRequiredService<VM_ForGlavnaya>(); 

        public VM_Settings VM_Settings => App.Host.Services.GetRequiredService<VM_Settings>();

        public VMWindowForUZ1 VMWindowForUZ1 => App.Host.Services.GetRequiredService<VMWindowForUZ1>(); //Transient

        public static void InitScopeUZ1()
        {
            scopeUZ1?.Dispose();
            scopeUZ1 = App.Host.Services.CreateScope();
        }

        public VM_Pledgor VM_Pledgor => scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>();
        public VM_Pledgee VM_Pledgee => scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>();
        public VM_Property VM_Property => scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>();
        public VM_Contract VM_Contract => scopeUZ1.ServiceProvider.GetRequiredService<VM_Contract>();
        public VM_Applicant VM_Applicant => scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>();


        public static void InitScopeUP1()
        {
            scopeUP1?.Dispose();
            scopeUP1 = App.Host.Services.CreateScope();
        }

        public VM_For_Win_UP1 VM_For_Win_UP1 => scopeUP1.ServiceProvider.GetRequiredService<VM_For_Win_UP1>();

    }
}
