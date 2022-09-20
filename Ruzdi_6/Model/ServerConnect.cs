using System.ComponentModel;

namespace Ruzdi_6.Model
{
    public class ServerConnect : ViewModel.ViewModel, IDataErrorInfo
    {
        private string server;
        private string user;
        private string database;
        private string password;
        private string port;
        private string pathCSP;

        public ServerConnect()
        {
        }

        public string Server { get => server; set => Set(ref server, value.Trim()); }
        public string User { get => user; set => Set(ref user, value.Trim()); }
        public string Database { get => database; set => Set(ref database, value.Trim()); }
        public string Password { get => password; set => Set(ref password, value.Trim()); }
        public string Port { get => port; set => Set(ref port, value.Trim()); }
        public string PathCSP
        {
            get => pathCSP;
            set => Set(ref pathCSP, value.Trim());
        }


        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "PathCSP":
                        if (String.IsNullOrEmpty(PathCSP))
                        {
                            error = "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                }
                return error;
            }
        }
    }
}
