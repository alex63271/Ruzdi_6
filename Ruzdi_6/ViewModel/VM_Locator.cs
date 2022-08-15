using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ruzdi_6.ViewModel
{
    public class VM_Locator
    {
        public VM_Locator()
        {
        }

        public VM_ForGlavnaya VM_ForGlavnaya => App.Services.GetRequiredService<VM_ForGlavnaya>();
        public VM_Pledgor VM_Pledgor => App.Services.GetRequiredService<VM_Pledgor>();
        public VMWindowForUZ1 VMWindowForUZ1 => App.Services.GetRequiredService<VMWindowForUZ1>();
        public VM_Pledgee VM_Pledgee => App.Services.GetRequiredService<VM_Pledgee>();
        public VM_Property VM_Property => App.Services.GetRequiredService<VM_Property>();
        public VM_Contract VM_Contract => App.Services.GetRequiredService<VM_Contract>();
        public VM_Applicant VM_Applicant => App.Services.GetRequiredService<VM_Applicant>();
        public VM_For_Win_UP1 VM_For_Win_UP1 => App.Services.GetRequiredService<VM_For_Win_UP1>();
        
    }
}
