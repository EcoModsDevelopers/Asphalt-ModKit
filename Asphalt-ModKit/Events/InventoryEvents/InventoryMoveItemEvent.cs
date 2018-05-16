using Asphalt.Api.Event;

namespace Asphalt.Events.InventoryEvents
{
    /// <summary>
    /// Called when an Item in an Inventory gets moved
    /// </summary>
    public class InventoryMoveItemEvent : CancellableEvent
    {

        public InventoryMoveItemEvent() : base()
        {
            
        }
    }

    internal class InventoryMoveItemEventHelper
    {
        public static bool Prefix(ref string methodname, object __result)
        {
            if (methodname != "MoveItems")
                return true;

            InventoryMoveItemEvent imie = new InventoryMoveItemEvent();
            IEvent imieEvent = imie;

            EventManager.CallEvent(ref imieEvent);

            if (imie.IsCancelled())
            {
                __result = null;
                return false;
            }

            return true;
        }
    }
}
