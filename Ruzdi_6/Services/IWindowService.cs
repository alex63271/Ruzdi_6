using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruzdi_6.Services
{
    public interface IWindowService
    {
        void ShowWindowDialog(object p);
        void CloseWindowDialog(object p);
    }
}
