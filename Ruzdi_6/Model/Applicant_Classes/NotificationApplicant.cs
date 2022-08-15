using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Applicant_Classes
{
    public class NotificationApplicant
    {
        public NotificationApplicant()
        {
        }
        public byte Role { get; set; }
        public ApplicantOrganization Organization { get; set; }
        public ApplicantPrivatePerson PrivatePerson { get; set; }
        public ApplicantAttorney Attorney { get; set; }
        public bool ShouldSerializeRole() => Role != 0;
    }
}
