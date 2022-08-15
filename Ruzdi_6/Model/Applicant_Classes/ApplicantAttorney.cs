using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Applicant_Classes
{
    public class ApplicantAttorney : IDataErrorInfo
    {
        public ApplicantAttorney()
        {
        }

        public ApplicantAttorneyName Name { get; set; }


        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public System.DateTime BirthDate { get; set; }


        public string Authority
        {
            get => authority;
            set
            {
                authority = value;
                string pattern = @"^[\w \s \W]{1,100}$";
                errors["Authority"] = !Regex.IsMatch(Authority, pattern) ? "Текст сообщения об ошибке" : null;
            }
        }

        public ApplicantAttorneyPersonDocument PersonDocument { get; set; }


        public ApplicantAttorneyPersonAddress PersonAddress { get; set; }

        public string Error => throw new System.NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
        private string authority;
    }
}
