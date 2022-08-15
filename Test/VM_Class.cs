using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class VM_Class
    {
        public VM_Class()
        {
            GlobalForeColor = "Red";
            ItemsFromModel = new Model[]
            {
                new Model
                {
                    ModelBody ="ModelBody",
                    ModelHeader = "ModelHeader",                                        
                },
                new Model
                {
                    ModelBody ="ModelBody1",
                    ModelHeader = "ModelHeader1"
                },
                new Model
                {
                    ModelBody ="ModelBody2",
                    ModelHeader = "ModelHeader2"
                }
            };
        }
        public Model[] ItemsFromModel { get; set; }
        public string GlobalForeColor { get; set; }
    }
}
