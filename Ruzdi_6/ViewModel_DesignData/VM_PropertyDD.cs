using Ruzdi_6.Model.Property_Classes;
using System.Collections.ObjectModel;

namespace Ruzdi_6.ViewModel_DesignData
{
    public class VM_PropertyDD
    {
        public VM_PropertyDD()
        {
            PersonalProperty = new ObservableCollection<PersonalProperty>
                {
                     new VehicleProperty
                     {
                         VIN = "vin-number",
                         PIN="PIN",
                         BodyNumber = "BodyNumber",
                         ChassisNumber = "ChassisNumber",
                         Description = "decript"
                     },
                     new OtherProperty
                     {
                         ID="Id-объекта",
                         Description ="описание иного имущества"
                     }
                };

            SelectProperty = new OtherProperty
            {
                ID = "Id-объекта",
                Description = "описание иного имущества"
            };
            /*SelectProperty = new VehicleProperty
            {
                VIN = "vin-number",
                PIN = "PIN",
                BodyNumber = "BodyNumber",
                ChassisNumber = "ChassisNumber",
                Description = "decript"
            };*/
        }

        public ObservableCollection<PersonalProperty> PersonalProperty { get; set; }

        public bool IsView { get; set; }

        #region SelectProperty- поле и св-во выбранного из списка имущества
        private PersonalProperty selectProperty;

        public PersonalProperty SelectProperty { get; set; }
        #endregion
    }
}
