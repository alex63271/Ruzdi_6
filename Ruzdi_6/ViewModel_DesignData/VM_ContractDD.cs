using Ruzdi_6.Model.Pledgee_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.ViewModel_DesignData
{
    public class VM_ContractDD
    {
        public VM_ContractDD()
        {
            Contract = new PledgeContract
            {
                Date = DateTime.Now,
                Name = "Наименование",
                Number = "Номер договора",
                TermOfContract = DateTime.Now
            };
        }
        public PledgeContract Contract { get; set; }
    }
}
