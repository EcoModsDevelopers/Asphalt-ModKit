

using Asphalt.api.Event;
/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Asphalt.plugin;

namespace Asphalt.api
{
    public class AsphaltPlugin : BasePlugin
    {
        private static AsphaltPlugin instance;
        private EventService eventService;

        //Initialization

        public override void OnInitialization()
        {
            this.eventService = new EventService();
            this.eventService.RegisterListener(new TestListener());
        }

        public AsphaltPlugin()
        {
            OnInitialization();
        }

        //Instance

        public static AsphaltPlugin Instance
        {
            get
            {
                if (instance == null)
                    instance = new AsphaltPlugin();
                return instance;
            }
        }

        //Managers & Services

        public EventService GetEventService()
        {
            return this.eventService;
        }
    }
}
