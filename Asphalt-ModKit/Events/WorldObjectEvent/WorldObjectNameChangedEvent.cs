namespace Asphalt.Api.Event.PlayerEvents
{
    /*
    public class WorldObjectNameChangedEvent : CancellableEvent
    {
        public User User { get; set; }

        public WorldObject WorldObject { get; set; }

        public WorldObjectNameChangedEvent(User pUser, WorldObject pWorldObject) : base()
        {
            User = pUser;
            WorldObject = pWorldObject;
        }
    }



    [HarmonyPatch(typeof(WorldObject))]
    [HarmonyPatch("OnNameChanged", PropertyMethod.Setter)]
    //  [HarmonyPatch("UpdateEnabledAndOperating")]
    static class AsphaltWorldObjectPatches
    {
        public static void Postfix() //WorldObject __instance, ref ThreadSafeAction __result
        {
       //     WorldObjectNameChangedEvent cEvent = new WorldObjectNameChangedEvent(null, __instance);
        //    IEvent iEvent = cEvent;

       //     EventManager.CallEvent(ref iEvent);
        }
    }



    /*
    internal class WorldObjectNameChangedEventHelper
    {
        public IAtomicAction CreateAtomicAction(User user, AirPollutionComponent obj, float value)
        {
            WorldPolluteEvent wpe = new WorldPolluteEvent(user, obj, value);
            IEvent wpeEvent = wpe;

            EventManager.CallEvent(ref wpeEvent);

            if (!wpe.IsCancelled())
                return CreateAtomicAction_original(wpe.User, wpe.Component, wpe.Value);

            return new FailedAtomicAction(new LocString("Asphalt " + nameof(WorldPolluteEvent)));
        }

        public IAtomicAction CreateAtomicAction_original(User user, AirPollutionComponent obj, float value)
        {
            throw new InvalidOperationException();
        }
    }*/
}
