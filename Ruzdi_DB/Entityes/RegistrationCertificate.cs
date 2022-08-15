using Ruzdi_Interfaces;

namespace Ruzdi_DB.Entityes
{
    public class RegistrationCertificate : IEntity
    {
        public string Id { get; set; }

        public string documentAndSignature { get; set; }
        //public string Notificationid { get; set; }
        
    }
}
