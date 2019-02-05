using Eco.Shared.Localization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Util
{
    public static class AsphaltLog
    {
        public static void Write(string message)
        {
            Log.Write(new LocString(message));
        }

        public static void WriteError(string message)
        {
            Log.WriteError(new LocString(message));
        }

        public static void WriteError(string message, Exception ex)
        {
            Log.WriteError(new LocString(message), ex);
        }

        public static void WriteErrorLine(string message)
        {
            Log.WriteErrorLine(new LocString(message));
        }

        public static void WriteException(Exception ex)
        {
            Log.WriteException(ex);
        }

        public static void WriteLine(string message)
        {
            Log.WriteLine(new LocString(message));
        }
    }
}
