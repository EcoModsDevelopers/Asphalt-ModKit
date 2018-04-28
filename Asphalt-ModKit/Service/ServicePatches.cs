using Asphalt.Util;
using Eco.Core.Serialization;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Service
{
    [HarmonyPatch(typeof(EcoSerializer), "FinishDeserialization")]
    internal static class PreEnablePatch
    {
        static void Postfix()
        {
            ServiceHelper.CallMethod("OnPreEnable");
        }
    }
}
