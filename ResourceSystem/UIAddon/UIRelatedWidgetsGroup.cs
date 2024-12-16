using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOUIEditor.ResourceSystem
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UIRelatedWidgetsGroup
    {
        public string groupName { get; set; }
        public UIRelatedWidgets widgets { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
