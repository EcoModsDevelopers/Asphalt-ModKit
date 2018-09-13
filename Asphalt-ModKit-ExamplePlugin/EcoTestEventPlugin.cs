using Eco.Core.Plugins.Interfaces;
using Asphalt.Api.Event;

namespace EcoTestEventPlugin
{
    public class EcoTestEventPlugin : IModKitPlugin, IServerPlugin
    {
        public static TestEventHandlers TestListener { get; protected set; }

        public EcoTestEventPlugin()
        {
            TestListener = new TestEventHandlers();

            EventManager.RegisterListener(TestListener);
        }

        public string GetStatus()
        {
            return "";
        }

        public override string ToString()
        {
            return "EcoTestEventPlugin";
        }
    }
}
