using Asphalt.Api.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoTestEventPlugin.util
{
    static class EventUtil
    {
        public static string EventToString(IEvent obj)
        {
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(obj);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("--------");
            builder.AppendLine(string.Format("{0}[", obj.GetType().Name));
            foreach (PropertyDescriptor pd in coll)
                builder.AppendLine(string.Format("'{0}' = '{1}'", pd.Name, pd.GetValue(obj)?.ToString()));
            builder.AppendLine("]");
            builder.AppendLine("--------");
            return builder.ToString();
        }
    }
}
