using Ruzdi_6.Model.Pledgee_Classes;

namespace Ruzdi_6.ViewModel
{
    public class VM_Contract : ViewModel
    {
        public VM_Contract()
        {
            if (App.DesignMode)
            {
                Contract = new PledgeContract
                {
                    Date = DateTime.Now,
                    Name = "Наименование",
                    Number = "Номер договора",
                    TermOfContract = DateTime.Now
                };
            }
            else
            {
                Contract = new PledgeContract
                {
                    Date = DateTime.Now,
                    TermOfContract = DateTime.Now,
                    Number = "",
                    Name = ""
                };
            }
        }

        

        private PledgeContract contract;
        public PledgeContract Contract
        {
            get => contract;
            set => Set(ref contract, value);
        }

        public bool IsView { get; set; }

        
    }
}
