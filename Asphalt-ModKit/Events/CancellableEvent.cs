using Asphalt.Api.Event;

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
