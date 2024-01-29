
using System.ComponentModel.DataAnnotations;

namespace Ruzdi_DB.Entityes
{
    public class Notification
    {
        public DateTime? DataTime { get; set; }

        public string Packageguid { get; set; }

        public string? Packageid { get; set; }

        public string Id { get; set; }


        public List<Pledgor>? Pledgors { get; set; } 

        #region свойства связи один-ко многим PledgeContract
        public int? ContractsID { get; set; }

        public Contracts? PledgeContract { get; set; }
        #endregion

        public string ZipArchive { get; set; }
        public string? NumberNotification { get; set; }
        public string TypeNotification { get; set; }
        public string Status { get; set; }
        public string? Error { get; set; }
        public string ThumbprintCert { get; set; }  
        public RegistrationCertificate? registrationCertificate { get; set; }

    }
}
