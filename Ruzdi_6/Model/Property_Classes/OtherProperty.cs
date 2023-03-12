using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Ruzdi_6.Model.Property_Classes
{
    public class OtherProperty : PersonalProperty, IDataErrorInfo
    {
        public OtherProperty() { }


        public string this[string propertyName] => !errors.ContainsKey(propertyName) ? null : errors[propertyName];

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();

        private string iD;

        private string description;

        public string ID
        {
            get => iD;
            set
            {
                iD = value;
                string patternID = @"^[\w \s \W]{0,25}$";
                if(ID!=null)
                errors["ID"] = !Regex.IsMatch(ID, patternID) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                string patternDescription = @"^[\w \s \W]{1,4000}$";
                errors["Description"] = !Regex.IsMatch(Description, patternDescription) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string Error => throw new System.NotImplementedException();
    }
}
