using Ruzdi_Interfaces;

namespace Ruzdi_DB.Entityes
{
    public class Persons : IEntity
    {
        public string Id { get; set; }

        public string Last { get; set; }
        public string First { get; set; }
        public string? Middle { get; set; }
        public DateTime BirthDate { get; set; }
        public uint PersonDocument { get; set; }
        public Regions Region { get; set; }
        
    }
}
