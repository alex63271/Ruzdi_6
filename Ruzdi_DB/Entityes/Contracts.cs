

namespace Ruzdi_DB.Entityes
{
    public class Contracts
    {        
        public string Id { get; set; }        

        public DateOnly Data { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public DateOnly TermOfContract { get; set; }

        public List<Notification> Notifications { get; set; } = new();
    }
}
