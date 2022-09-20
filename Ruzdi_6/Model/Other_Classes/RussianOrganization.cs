using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Other_Classes
{
    public class RussianOrganization : IDataErrorInfo
    {
        private string oGRN;
        private string iNN;
        private string nameFull;
        private string email;

        public RussianOrganization() { }


        public string NameFull
        {
            get => nameFull;
            set
            {
                string patternNameFull = @"^[\w \s \W]{1,1000}$";
                nameFull = value;
                errors["NameFull"] = !Regex.IsMatch(NameFull, patternNameFull) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string OGRN
        {
            get => oGRN;
            set
            {
                string patternOGRN = @"^[\d+]{13}$";
                oGRN = value.Trim();
                errors["OGRN"] = !Regex.IsMatch(OGRN, patternOGRN) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string INN
        {
            get => iNN;
            set
            {
                string patternINN = @"\b([0-9]{1}[1-9]{1}|[1-9]{1}[0-9]{1})[0-9]{8}\b";
                iNN = value.Trim();
                errors["INN"] = !Regex.IsMatch(INN, patternINN) ? "Текст сообщения об ошибке" : null;
            }
        }
        public RussianOrganizationAddress Address { get; set; }
        public string Email
        {
            get => email;
            set
            {
                string patternEmail = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
                email = value;
                errors["Email"] = (!Regex.IsMatch(Email, patternEmail) && Email.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string Error => throw new System.NotImplementedException();

        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();

    }
}
