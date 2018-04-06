/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 29, 2018
* ------------------------------------
**/

using Asphalt.api.Event;
using Asphalt.plugin;
using Eco.Core.Plugins.Interfaces;

namespace Asphalt.api
{
    public class Asphalt : IModKitPlugin, IServerPlugin
    {
        bool initialized;

        public Asphalt()
        {
            EventManager.Instance.RegisterListener(new TestListener());

            this.initialized = true;
        }

        public string GetStatus()
        {
            return initialized ? "Complete!" : "Initializing...";
        }

        public override string ToString()
        {
            return "Asphalt ModKit";
        }
    }
}
