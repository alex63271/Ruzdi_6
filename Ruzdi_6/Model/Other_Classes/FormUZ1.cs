using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using Ruzdi_6.Model.Property_Classes;
using System.Collections.ObjectModel;

namespace Ruzdi_6.Model.Other_Classes
{
    public class FormUZ1
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
        
    }
}
