using Ruzdi_Interfaces;

namespace Ruzdi_DB.Entityes
{
    public class Organizations : IEntity
    {
        public string Id { get; set; }

        public string NameFull { get; set; }
        public uint OGRN { get; set; }
        public uint INN { get; set; }
        public string? Email { get; set; }
        public Regions Region { get; set; }
        
    }
}
