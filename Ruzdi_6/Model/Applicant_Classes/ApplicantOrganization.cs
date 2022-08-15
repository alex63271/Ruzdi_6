using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Applicant_Classes
{
    public class ApplicantOrganization : IDataErrorInfo
    {
        public ApplicantOrganization()
        {
        }
        public string NameFull
        {
            get => nameFull;
            set
            {
                nameFull = value;
                string patternNameFull = @"^[\w \s \W]{1,1000}$";
                errors["NameFull"] = !Regex.IsMatch(NameFull, patternNameFull) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string URN
        {
            get => uRN;
            set
            {
                uRN = value.Trim();
                string patternOGRN = @"^[\d+]{13}$";
                errors["URN"] = !Regex.IsMatch(URN, patternOGRN) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string UINN
        {
            get => uINN;
            set
            {
                uINN = value.Trim();
                string patternINN = @"\b([0-9]{1}[1-9]{1}|[1-9]{1}[0-9]{1})[0-9]{8}\b";
                errors["UINN"] = !Regex.IsMatch(UINN, patternINN) ? "Текст сообщения об ошибке" : null;
            }
        }
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
        private string uINN;
        private string uRN;
        private string nameFull;
    }
}
