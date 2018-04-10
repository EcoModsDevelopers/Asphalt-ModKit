using Asphalt.Api.Event;
using Asphalt.Api.Event.PlayerEvents;
using Eco.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events
{
    public class AsphaltThreadSafeAction : ThreadSafeAction
    {
        public AsphaltThreadSafeAction()
        {

        }

        public void AsphaltInvoke()
        {
            if (this.GetType() != typeof(AsphaltThreadSafeAction))
            {
                Invoke_original();
                return;
            }


            WorldObjectNameChangedEvent wvt = new WorldObjectNameChangedEvent(null);
            Api.Event.IEvent iEvent = wvt;

            EventManager.CallEvent(ref iEvent);


            Invoke_original();

            // FieldInfo fi = typeof(ThreadSafeAction).GetField("action", BindingFlags.Instance | BindingFlags.NonPublic);
            //  Action reflAction = fi.GetValue(this) as Action;
            //    reflAction?.Invoke();
        }

        public void Invoke_original()
        {
            throw new InvalidOperationException();
        }
    }
}
