using Asphalt.Api.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events
{
    public abstract class CancellableEvent : ICancellable, IEvent
    {
        private bool mIsCancelled = false;

        public bool IsCancelled()
        {
            return mIsCancelled;
        }

        public void SetCancelled(bool pCancelled)
        {
            mIsCancelled = pCancelled;
        }
    }
}
