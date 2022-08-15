using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Property_Classes
{
    public class PersonalProperty
    {
        public PersonalProperty()
        {
        }
        
        public VehicleProperty VehicleProperty { get; set; }
      
        public OtherProperty OtherProperty { get; set; }
    }
}
