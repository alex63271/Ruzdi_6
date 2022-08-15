using Ruzdi_6.Model.Other_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Pledgor_Classes;

public class PledgorOrganization : Pledgor
{
    public PledgorOrganization()
    {
    }
    public RussianOrganization RussianOrganization { get; set; }
}
