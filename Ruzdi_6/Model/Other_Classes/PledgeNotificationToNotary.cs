using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Other_Classes
{
    public class PledgeNotificationToNotary
    {
        public PledgeNotificationToNotary()
        {

        }
        public string NotificationId { get; set; }

        public NotificationData NotificationData { get; set; }

        [System.Xml.Serialization.XmlAttribute()]
        public decimal version { get; set; }
    }
}

