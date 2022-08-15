using Ruzdi_6.Model.Other_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Applicant_Classes
{
    public class ApplicantPrivatePerson : NotificationApplicant, IDataErrorInfo
    {

        public ApplicantPrivatePerson()
        {
        }
        public PrivatePersonName Name { get; set; }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                string patternEmail = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
                errors["Email"] = !Regex.IsMatch(Email, patternEmail) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
        private string email;
    }
}
