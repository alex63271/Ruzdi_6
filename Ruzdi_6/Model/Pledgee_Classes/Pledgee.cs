using Ruzdi_6.Model.Other_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Pledgee_Classes
{
    public class Pledgee : FormUZ1
    {
        public Pledgee()
        {
        }
        public PledgeePrivatePerson PrivatePerson { get; set; }
        public PledgeeOrganization Organization { get; set; }
    }
}
