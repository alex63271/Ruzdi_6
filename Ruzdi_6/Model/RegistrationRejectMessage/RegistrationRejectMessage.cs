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
