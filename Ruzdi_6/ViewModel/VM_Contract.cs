using Ruzdi_6.Model.Pledgee_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        #region Поля и методы Singletone
        private static VM_Contract VM_Contract_UZ1;
        public static VM_Contract GetVM_Contract_UZ1()
        {
            if (VM_Contract_UZ1 == null)
            {
                VM_Contract_UZ1 = new VM_Contract();
            }
            return VM_Contract_UZ1;
        }

        public static void SetNull_VM_Contract()
        {
            VM_Contract_UZ1 = null;
        }
        #endregion

        private PledgeContract contract;
        public PledgeContract Contract
        {
            get => contract;
            set => Set(ref contract, value);
        }

        public bool IsView { get; set; }

        #region Свойство для дизайнера
        public static VM_Contract VM_Contract_ForDesiner
        {
            get
            {
                if (vM_Contract_ForDesiner == null)
                {
                    vM_Contract_ForDesiner = new VM_Contract();
                }
                return vM_Contract_ForDesiner;
            }
        }
        private static VM_Contract vM_Contract_ForDesiner;
        #endregion
    }
}
