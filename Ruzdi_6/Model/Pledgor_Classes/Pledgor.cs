using Ruzdi_6.Model.Other_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Pledgor_Classes
{
    public class Pledgor : FormUZ1
    {
        public Pledgor()
        {
        }
        public PledgorOrganization Organization { get; set; }

        public PledgorPrivatePerson PrivatePerson { get; set; }
    }
}
