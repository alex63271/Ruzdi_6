using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_DB.Entityes
{
    public class Regions
    {
        public string Region { get; set; }
        [Key]     
        
        public string CodeRegion { get; set; }
        
        //public string Id { get; set; }

        public List<Persons>? Persons { get; set; } = new();

        public List<Organizations>? Organizations { get; set; } = new();
    }
}
