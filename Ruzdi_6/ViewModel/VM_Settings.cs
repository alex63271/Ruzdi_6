using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.ViewModel
{
    public class VM_Settings : ViewModel
    {
        private readonly IConfiguration configuration;
        private string csp_path;

        public VM_Settings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Csp_path
        {
            get => App.GetPathCspTestFromConfig();
            set
            {
                //configuration.
                Set(ref csp_path, value);
            }
        }




    }
}
