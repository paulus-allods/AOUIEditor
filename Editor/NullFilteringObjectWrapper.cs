using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOUIEditor
{
    public class NullFilteringObjectWrapper : ICustomTypeDescriptor
    {
        private readonly object instance;

        public NullFilteringObjectWrapper(object instance)
        {
            this.instance = instance;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(instance);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(instance);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(instance);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(instance);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(instance);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(instance);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(instance, editorBaseType);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(instance);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(instance, attributes);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return new NullFilteringTypeDescriptor(instance).GetProperties(attributes);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return instance;
        }
    }
}
