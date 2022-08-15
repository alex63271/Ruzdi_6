using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruzdi_6.Model.Applicant_Classes
{
    public class ApplicantAttorneyPersonDocument : IDataErrorInfo
    {
        private string seriesNumber;

        public ApplicantAttorneyPersonDocument()
        {
        }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                string pattern = @"^[\d+]{10}$";
                switch (columnName)
                {
                    case "SeriesNumber":
                        if (!Regex.IsMatch(SeriesNumber, pattern))
                        {
                            error = "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                }
                return error;
            }
        }
        public byte Code { get; set; }
        public string Name { get; set; }
        public string SeriesNumber { get => seriesNumber; set => seriesNumber = value.Trim(); }
        public string Error => throw new System.NotImplementedException();
    }
}
