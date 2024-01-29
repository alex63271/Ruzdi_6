

namespace Ruzdi_DB.Entityes
{
    public class Organizations : Pledgor
    {
        public int Id { get; set; }

        public string NameFull { get; set; }
        public string OGRN { get; set; }
        public string INN { get; set; }
        public string? Email { get; set; }
        public Regions Region { get; set; }
      

    }
}
