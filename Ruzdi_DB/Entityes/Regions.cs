using Microsoft.EntityFrameworkCore;
using Ruzdi_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_DB.Entityes
{
    public class Regions : IEntity
    {
        public string Region { get; set; }
        [Key]
        public string CodeRegion { get; set; }
        
        public string Id { get; set; }
    }
}
