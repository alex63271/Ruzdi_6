using Ruzdi_Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Ruzdi_DB.Entityes
{
    public class Pledgor : IEntity
    {
        public string Id { get; set; }

        public Persons? Person { get; set; }
        public Organizations? Organization { get; set; }    
    }
}
