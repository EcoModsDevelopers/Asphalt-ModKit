/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;

namespace Asphalt.api
{
    public abstract class BasePlugin : IInitializablePlugin
    {
        private bool initialized;

        //Initialization

        public string GetStatus()
        {
            return this.initialized ? "Complete" : "Loading";
        }

        public void Initialize()
        {
            OnInitialization();

            this.initialized = true;
        }

        public void Initialize(TimedTask tt)
        {
            OnInitialization();

            this.initialized = true;
        }

        public abstract void OnInitialization();
    }
}
