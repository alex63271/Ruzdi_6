using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Applicant_Classes
{
    public class ApplicantAttorneyName : IDataErrorInfo
    {
        public ApplicantAttorneyName()
        {
        }
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public string Last
        {
            get => last;
            set
            {
                last = value;
                errors["Last"] = !Regex.IsMatch(Last, pattern) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string First
        {
            get => first;
            set
            {
                first = value;
                errors["First"] = !Regex.IsMatch(First, pattern) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string Middle
        {
            get => middle;
            set
            {
                middle = value;
                errors["Middle"] = (!Regex.IsMatch(Middle, pattern) && Middle.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string Error => throw new NotImplementedException();

        public bool ShouldSerializeMiddle() => !string.IsNullOrEmpty(Middle);

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
        string pattern = @"^[А-Яа-яёЁ \s]{1,60}$";
        private string last;
        private string first;
        private string middle;
    }
}
