
using System.ComponentModel.DataAnnotations;

namespace Ruzdi_DB.Entityes
{
    public class Notification
    {
        public DateTime? DataTime { get; set; }

        public string Packageguid { get; set; }

        public string? Packageid { get; set; }

        public string Id { get; set; }


        public List<Pledgor>? Pledgors { get; set; } //= new();

        #region свойства связи один-ко многим PledgeContract
        public string? ContractsID { get; set; }

        public Contracts? PledgeContract { get; set; }// = new();
        #endregion

        public string ZipArchive { get; set; }
        public string? NumberNotification { get; set; }
        public string TypeNotification { get; set; }
        public string Status { get; set; }
        public string? Error { get; set; }
        public RegistrationCertificate? registrationCertificate { get; set; }

    }
}
