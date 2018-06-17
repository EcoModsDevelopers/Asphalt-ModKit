using Asphalt.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return (T) (pType.GetType().GetProperty(pfName).GetValue(pType) ?? pType.GetType().GetField(pfName).GetValue(pType));
        }
    }
}
