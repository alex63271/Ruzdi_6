using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using Ruzdi_6.Model.Property_Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Other_Classes
{
    public class FormUZ1 : INotifyPropertyChanged
    {

        public FormUZ1()
        {
        }


        #region коллекция имущества

        public ObservableCollection<PersonalProperty> PersonalProperties { get; set; }
        #endregion

        #region коллекция залогодателей      

        
        public ObservableCollection<Pledgor> Pledgors { get; set; }
        #endregion

        #region Коллекция залогодержателей


 
        public ObservableCollection<Pledgee> Pledgee { get; set; }
        #endregion
        public PledgeContract PledgeContract { get; set; }
        public NotificationApplicant NotificationApplicant { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
