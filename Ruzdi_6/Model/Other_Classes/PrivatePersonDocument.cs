using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Other_Classes
{
    public class PrivatePersonDocument : IDataErrorInfo
    {
        private string seriesNumber;

        public PrivatePersonDocument()
        {
        }
        string pattern = @"^[\d+]{10}$";
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public byte Code { get; set; }
        public string Name { get; set; }
        public string SeriesNumber
        {
            get => seriesNumber;
            set
            {
                seriesNumber = value.Trim();
                errors["SeriesNumber"] = !Regex.IsMatch(SeriesNumber, pattern) ? "Текст сообщения об ошибке" : null;
            }
        }

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
        public string Error => throw new NotImplementedException();
    }
}
