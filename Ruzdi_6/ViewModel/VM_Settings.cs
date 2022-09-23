using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Ruzdi_6.ViewModel
{
    public class VM_Settings : ViewModel
    {
        private readonly string connstring;

        public VM_Settings(IConfiguration configuration)
        {
            connstring = configuration.GetSection("Database").GetConnectionString("MySql");
        }

        public string Csp_path => App.GetPathCspTestFromConfig();


        public string AdressMysql
        {
            get
            {
                string s = "server=";
                return connstring.Substring(connstring.IndexOf(s) + s.Length, connstring.IndexOf(";", connstring.IndexOf(s)) - (connstring.IndexOf(s) + s.Length));
            }
        }

        public string DbName
        {
            get
            {
                string s = "database=";
                return connstring.Substring(connstring.IndexOf(s) + s.Length, connstring.IndexOf(";", connstring.IndexOf(s)) - (connstring.IndexOf(s) + s.Length));
            }
        }
    }
}
