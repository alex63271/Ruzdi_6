using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Pledgee_Classes
{
    public class PledgeContract : IDataErrorInfo
    {
        public PledgeContract()
        {
        }

        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
        private string name;
        private string number;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                string pattern4000 = @"\b^[\w \s \W]{1,4000}$\b";
                errors["Name"] = !Regex.IsMatch(Name, pattern4000) ? "Текст сообщения об ошибке" : null;
            }
        }
        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public System.DateTime Date { get; set; }
        public string Number
        {
            get => number;
            set
            {
                number = value;
                string pattern60 = @"\b^[\w \s \W]{1,60}$\b";
                errors["Number"] = !Regex.IsMatch(Number, pattern60) ? "Текст сообщения об ошибке" : null;
            }
        }
        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public System.DateTime TermOfContract { get; set; }

        public string Error => throw new NotImplementedException();
    }
}
