using Asphalt.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Util
{
    public static class ReflectionUtil
    {
        public static IEnumerable<PropertyFieldInfo> GetPropertyFieldInfos(Type pServerPlugin, Type pType)
        {
            return pServerPlugin.GetProperties().Where(x => x.PropertyType == pType).Select(x => new PropertyFieldInfo(x)).Concat(pServerPlugin.GetFields().Where(x => x.FieldType == pType).Select(x => new PropertyFieldInfo(x)));
        }

        public static T GetPropertyFieldValue<T>(object pType, string pfName)
        {
            return (T)(pType.GetType().GetProperty(pfName).GetValue(pType) ?? pType.GetType().GetField(pfName).GetValue(pType));
        }
        /*
        public static void GetTypesRecursive(object obj, ref IList<Type> pTypes)
        {
            if (obj == null)
            {
                return;
            }

            Type objType = obj.GetType();

            Debug.WriteLine(objType);

            if (IsSimpleType(objType))
            {
                pTypes.Add(objType);
                return;
            }

            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);
                if (IsSimpleType(property.PropertyType))
                {
                    //  Console.WriteLine("{0}{1}: {2}", indentString, property.Name, propValue);
                    pTypes.Add(property.PropertyType);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    //   Console.WriteLine("{0}{1}:", indentString, property.Name);
                    pTypes.Add(property.PropertyType);
                    IEnumerable enumerable = (IEnumerable)propValue;
                    foreach (object child in enumerable)
                    {
                        GetTypesRecursive(child, ref pTypes);
                    }
                }
                else
                {
                    // Console.WriteLine("{0}{1}:", indentString, property.Name);
                    GetTypesRecursive(propValue, ref pTypes);
                }
            }
        }

        public static bool IsSimpleType(Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[]
                {
                typeof(String),
                typeof(Decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid),
                typeof(string[]),
                typeof(int[]),
                typeof(double[]),
                typeof(float[]),
                typeof(decimal[]),
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        } */

    }
}
