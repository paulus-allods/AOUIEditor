using AOUIEditor.ResourceSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOUIEditor
{
    public class NullFilteringTypeDescriptor : CustomTypeDescriptor
    {
        private readonly object instance;

        public NullFilteringTypeDescriptor(object instance)
        {
            this.instance = instance;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance, attributes);
            PropertyDescriptorCollection filteredProperties = new PropertyDescriptorCollection(null);

            foreach (PropertyDescriptor property in properties)
            {
                object value = property.GetValue(instance);
                if (value != null) // && NotNullObject(value))
                {
                    if (value is string)
                    {
                        if (!string.IsNullOrEmpty(value as string))
                        {
                            filteredProperties.Add(property);
                        }
                    }
                    else
                    {
                        filteredProperties.Add(property);
                    }
                }
            }

            return filteredProperties;
        }

        // It doesn't work well.. =(
        private bool NotNullObject(object obj)
        {
            if (!obj.GetType().IsClass)
            {
                return true;
            }
            if (obj.GetType() == typeof(string))
            {
                if (!string.IsNullOrEmpty(obj as string))
                    return true;
            }
            var properties = obj.GetType().GetFilteredProperties();
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType.IsValueType)
                {
                    var v = propertyInfo.GetValue(obj);
                    if (v != null)
                        return true;
                }
                else if (propertyInfo.PropertyType == typeof(string))
                {
                    if (!string.IsNullOrEmpty(propertyInfo.GetValue(obj) as string))
                        return true;
                }
                else if (propertyInfo.PropertyType.IsArray)
                {
                    Array a = (Array)propertyInfo.GetValue(obj);
                    if (a != null && a.Length > 0)
                        return true;
                }
                else if (propertyInfo.PropertyType == typeof(XdbObject) || propertyInfo.PropertyType.IsSubclassOf(typeof(XdbObject)))
                {
                    var v = propertyInfo.GetValue(obj);
                    if (v != null)
                        return true;
                }
                else if (propertyInfo.PropertyType == typeof(TextObject) || propertyInfo.PropertyType.IsSubclassOf(typeof(TextObject)))
                {
                    var v = propertyInfo.GetValue(obj);
                    if (v != null)
                        return true;
                }
                else if (propertyInfo.PropertyType.IsClass)
                {
                    var v = propertyInfo.GetValue(obj);
                    if (v != null)
                    {
                        if (NotNullObject(v) == true)
                            return true;
                    }
                }
            }
            return false;
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(new Attribute[0]);
        }
    }
}
