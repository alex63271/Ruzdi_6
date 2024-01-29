
using System.ComponentModel.DataAnnotations;

namespace Ruzdi_DB.Entityes
{
    public class Pledgor 
    {
        public int? Id { get; set; }

        public List<Notification>? Notifications { get; set; }// свойство для отношения многие ко многим
    }
}
