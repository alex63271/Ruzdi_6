using Ruzdi_6.Model.Applicant_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Other_Classes
{
    public class FormUP1 : IDataErrorInfo
    {
        private string creationReferenceNumber;

        public FormUP1()
        {
        }

        public string CreationReferenceNumber
        {
            get => creationReferenceNumber;
            set
            {
                creationReferenceNumber = value.Trim();
                string pattern = @"\b[0-9]{4}-[0-9]{3}-[0-9]{6}-[0-9]{3}\b";
                errors["CreationReferenceNumber"] = !Regex.IsMatch(CreationReferenceNumber, pattern) ? "Текст сообщения об ошибке" : null;
            }
        }
        public NotificationApplicant NotificationApplicant { get; set; }

        public string Error => throw new System.NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
    }

}
