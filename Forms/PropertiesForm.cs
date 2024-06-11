using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOUIEditor
{
    public partial class PropertiesForm : DockForm
    {
        private static PropertiesForm Instance;

        public object currentObject;
        public object currentWrapper;

        public PropertiesForm()
        {
            InitializeComponent();

            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.Name = "propertyGrid";
            //propertyGrid1.PropertySort = PropertySort.NoSort;
            propertyGrid1.PropertyValueChanged += PropertyGrid_PropertyValueChanged;

            label1.Text = "";

            Instance = this;
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Project.UpdateInfo();
        }

        public static void RefreshGrid()
        {
            Instance.propertyGrid1.Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeState();
        }

        public void ChangeState()
        {
            if (checkBox1.Checked)
            {
                propertyGrid1.SelectedObject = currentWrapper;
            }
            else
            {
                propertyGrid1.SelectedObject = currentObject;
            }
        }

        public static object SelectedObject
        {
            get { return Instance.currentObject; }
            set
            {
                if (value != null)
                {
                    Instance.currentObject = value;
                    Instance.currentWrapper = new NullFilteringObjectWrapper(value);
                    Instance.ChangeState();
                }
                else
                {
                    Instance.currentObject = null;
                    Instance.currentWrapper = null;
                    Instance.propertyGrid1.SelectedObject = null;
                }
                if (value != null)
                {
                    Instance.label1.Text = value.ToString();
                }
                else
                {
                    Instance.label1.Text = "";
                }
            }
        }
    }
}
