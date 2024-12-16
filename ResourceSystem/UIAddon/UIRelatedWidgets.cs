using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOUIEditor.ResourceSystem
{
    public class UIRelatedWidgets : XdbObject
    {
        public UIWidgetResource[] items { get; set; }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UIWidgetResource
    {
        public string name { get; set; }
        public Widget widget { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
