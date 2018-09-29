using Asphalt.Events;
using Asphalt.Events.InventoryEvents;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Components;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Asphalt.Api.Event.RpcEvents
{
    /// <summary>
    /// Called when an RPC-Event gets invoked
    /// </summary>
    public class RpcInvokeEvent : CancellableEvent
    {
        public string Methodname { get; protected set; }
        public BSONObject Bson { get; protected set; }

        public RpcInvokeEvent(string methodname, BSONObject bson) : base()
        {
            Methodname = methodname;
            Bson = bson;
        }
    }

    internal class RpcInvokeEventHelper
    {
        public static bool Prefix(ref string methodname, ref BSONObject bson, object __result)
        {
            RpcInvokeEvent rie = new RpcInvokeEvent(methodname, bson);
            IEvent rieEvent = rie;

            EventManager.CallEvent(ref rieEvent);

            if (rie.IsCancelled())
            {
                __result = null;
                return false;
            }

            return true;
        }
    }
}
