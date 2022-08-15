using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ruzdi_6.Model.Other_Classes
{
    public class NotificationData
    {
        public NotificationData()   //конструктор для сериализации
        {
        }
        public FormUP1 FormUP1 { get; set; }
        public FormUZ1 FormUZ1 { get; set; }

        [XmlAttribute]
        public decimal version { get; set; }
    }
}
