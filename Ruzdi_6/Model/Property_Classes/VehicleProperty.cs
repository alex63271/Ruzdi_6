using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Ruzdi_6.Model.Property_Classes
{
    public class VehicleProperty : PersonalProperty, IDataErrorInfo
    {
        private string vIN;

        public VehicleProperty()
        {
        }
        public string VIN
        {
            get => vIN;
            set
            {
                vIN = value.ToUpper();
                CheckValidation();
            }
        }
        public string PIN
        {
            get => pIN;
            set
            {
                pIN = value;
                CheckValidation();
            }
        }
        public string ChassisNumber { get; set; }

        public string BodyNumber { get; set; }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                string patternDescription = @"^[\w \s \W]{1,1000}$";
                errors["Description"] = !Regex.IsMatch(Description, patternDescription) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string propertyName] => !errors.ContainsKey(propertyName) ? null : errors[propertyName];

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();

        void AddError(string propname, string error)
        {
            if (!errors.ContainsKey(propname))
            {
                errors.Add(propname, error);
            }
        }
        
        void CheckValidation()
        {
            if (string.IsNullOrEmpty(VIN) && string.IsNullOrEmpty(PIN))
            {
                AddError("PIN", "Текст сообщения об ошибке");
                AddError("VIN", "Текст сообщения об ошибке");
            }
            else if (!string.IsNullOrEmpty(VIN) && string.IsNullOrEmpty(PIN))
            {
                errors.Remove("VIN");
                errors.Remove("PIN");
                string patternVIN = @"\b[0-9A-HJ-NPR-Z]{11}\b|\b[0-9A-HJ-NPR-Z]{17}\b";
                if (!Regex.IsMatch(VIN, patternVIN))
                {
                    AddError("VIN", "Текст сообщения об ошибке");
                }
            }
            else if (string.IsNullOrEmpty(VIN) && !string.IsNullOrEmpty(PIN))
            {
                errors.Remove("VIN");
                errors.Remove("PIN");
                string patternPIN = @"^[\w \s \W]{1,25}$";
                if (!Regex.IsMatch(PIN, patternPIN))
                {
                    AddError("PIN", "Текст сообщения об ошибке");
                }
            }
        }
        
        private string pIN;
        
        private string description;

        #region Методы для сериализатора
        public bool ShouldSerializeVIN() => !string.IsNullOrEmpty(VIN);
        public bool ShouldSerializePIN() => !string.IsNullOrEmpty(PIN);
        public bool ShouldSerializeBodyNumber() => !string.IsNullOrEmpty(BodyNumber);
        public bool ShouldSerializeChassisNumber() => !string.IsNullOrEmpty(ChassisNumber);
        #endregion
    }
}
