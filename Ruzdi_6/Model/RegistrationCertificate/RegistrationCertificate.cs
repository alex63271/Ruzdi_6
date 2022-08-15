using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.RegistrationCertificate
{
    public class RegistrationCertificate
    {
        public string RegistrationCertificateId { get; set; }
        public string NotificationId { get; set; }
        public RegistrationCertificateData RegistrationCertificateData { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public decimal version { get; set; }
    }
}
