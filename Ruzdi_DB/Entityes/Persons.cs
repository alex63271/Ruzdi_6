

namespace Ruzdi_DB.Entityes
{
    public class Persons : Pledgor
    {
        public int Id { get; set; }

        public string Last { get; set; }
        public string First { get; set; }
        public string? Middle { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PersonDocument { get; set; }

        public Regions Region { get; set; }

        
        
    }
}
