using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.RegistrationRejectMessage
{
    public class RegistrationRejectMessage
    {
        public string RegistrationRejectId { get; set; }
        public string NotificationId { get; set; }
        public string RejectReason { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public decimal version { get; set; }
    }
}
