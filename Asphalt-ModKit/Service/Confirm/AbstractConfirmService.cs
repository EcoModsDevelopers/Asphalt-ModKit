/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 27, 2018
 * ------------------------------------
 **/

namespace Asphalt.Service.Confirm
{
    /*
    public abstract class AbstractConfirmService : IAspahltService, IConfirmable
    {
        private double timeout;
        private DateTime created = DateTime.Now;
        protected Task task;
        protected CancellationTokenSource cancelToken;

        private Dictionary<string, object> storage;

        public AbstractConfirmService(AsphaltMod mod) : base(mod)
        {
            this.Init();
        }

        private void Init(double timeout)
        {
            this.storage = new Dictionary<string, object>();
            this.SetTimeout(timeout);

            cancelToken = new CancellationTokenSource();
            task = Task.Delay(TimeSpan.FromSeconds(this.GetTimeout()), cancelToken.Token).ContinueWith(delegate
            {
                if(!cancelToken.IsCancellationRequested) this.Abort();
            });
        }

        public override void Init()
        {
            this.Init(30.0);
            //TODO: Defaults
        }

        public abstract void Abort();

        public abstract void Call();

        public void Invalidate()
        {
            this.cancelToken.Cancel();
        }

        public override void Reload()
        {
            //TODO: Log msg
        }

        
         // Storage
         
        public void Store(string key, object cs)
        {
            if (this.storage.ContainsKey(key))
                this.storage.Remove(key);

            this.storage.Add(key, cs);
        }

        public object Get(string key)
        {
            object value;
            return this.storage.TryGetValue(key, out value) ? value : null;
        }

        
        // Timeout
         

        public void SetTimeout(double timeout)
        {
            this.timeout = timeout;
        }

        public double GetTimeout()
        {
            return this.timeout;
        }

        public void CheckExeeded()
        {
            TimeSpan diff = DateTime.Now - created;
            if(diff.TotalSeconds > this.timeout)
            {
                this.Abort();
                throw new TimeoutException();
            }
        }
    } */
}
