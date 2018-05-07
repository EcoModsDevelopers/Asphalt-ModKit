using Asphalt.Storeable;
using System;
using System.Linq;
using System.Reflection;

namespace Asphalt.Service
{
    public class PropertyFieldInfo
    {
        public PropertyInfo PropertyInfo { get; protected set; }
        public FieldInfo FieldInfo { get; protected set; }

        public PropertyFieldInfo(PropertyInfo pPropertyInfo)
        {
            PropertyInfo = pPropertyInfo;
        }

        public PropertyFieldInfo(FieldInfo pFieldInfo)
        {
            FieldInfo = pFieldInfo;
        }

        public PropertyFieldInfo(Type pType, string pName)
        {
            PropertyInfo = pType.GetProperty(pName);
            FieldInfo = pType.GetField(pName);

            if (PropertyInfo == null && FieldInfo == null)
                throw new ArgumentNullException($"No public Field or Property with name {pName} found in type {pType.ToString()}");
        }

        public bool HasInjectAttribute()
        {
            return HasAttribute(typeof(InjectAttribute));
        }

        public bool HasDefaultValuesAttribute()
        {
            return HasAttribute(typeof(DefaultValuesAttribute));
        }

        public bool HasStorageLocationAttribute()
        {
            return HasAttribute(typeof(StorageLocationAttribute));
        }

        public bool HasAttribute(Type pAttributeType)
        {
            // It's mampfs fault, complain to him.
            if(FieldInfo == null)
                return PropertyInfo.CustomAttributes.Any(c => c.AttributeType == pAttributeType);
            return FieldInfo.CustomAttributes.Any(c => c.AttributeType == pAttributeType);
        }

        public DefaultValuesAttribute GetDefaultValuesAttribute()
        {
            return (DefaultValuesAttribute) GetAttribute(typeof(DefaultValuesAttribute));
        }

        public StorageLocationAttribute GetStorageLocationAttribute()
        {
            return (StorageLocationAttribute)GetAttribute(typeof(StorageLocationAttribute));
        }

        public Attribute GetAttribute(Type pAttributeType)
        {
            if (FieldInfo == null)
                return PropertyInfo.GetCustomAttribute(pAttributeType);
            return FieldInfo.GetCustomAttribute(pAttributeType);
        }

        public string GetName()
        {
            if (FieldInfo == null)
                return PropertyInfo.Name;
            return FieldInfo.Name;
        }

        public void SetValue(object obj, object value)
        {
            if (FieldInfo == null)
                PropertyInfo.SetValue(obj, value);
            else
                FieldInfo.SetValue(obj, value);
        }
    }
}
