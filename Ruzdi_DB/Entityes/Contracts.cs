using Ruzdi_Interfaces;

namespace Ruzdi_DB.Entityes
{
    public class Contracts  : IEntity
    {        
        public string Id { get; set; }        

        public DateTime Data { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public DateTime TermOfContract { get; set; }
    }
}
