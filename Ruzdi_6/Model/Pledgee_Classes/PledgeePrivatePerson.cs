using Ruzdi_6.Model.Other_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Pledgee_Classes
{
    public class PledgeePrivatePerson : Pledgee, IDataErrorInfo
    {
        public PledgeePrivatePerson() { }

        private string email;
       
        public PrivatePersonName Name { get; set; }

        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public DateTime BirthDate { get; set; }

        public PrivatePersonDocument PersonDocument { get; set; }

        public PrivatePersonPersonAddress PersonAddress { get; set; }

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
        public string Error => throw new NotImplementedException();

        public bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);

        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
    }
}
