using Asphalt.Api.Event;

namespace Asphalt.Events.InventoryEvents
{
    /// <summary>
    /// Called when the Player changes the Selected Hotbar Slot
    /// </summary>
    public class InventoryChangeSelectedSlotEvent : IEvent
    {

        public InventoryChangeSelectedSlotEvent()
        {
            
        }
    }

    internal class InventoryChangeSelectedSlotEventHelper
    {
        public static bool Prefix(ref string methodname, object __result)
        {
            if (methodname != "SelectIndex")
                return true;

            InventoryChangeSelectedSlotEvent csse = new InventoryChangeSelectedSlotEvent();
            IEvent csseEvent = csse;

            EventManager.CallEvent(ref csseEvent);

            return true;
        }
    }
}
