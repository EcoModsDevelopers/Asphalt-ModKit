using Asphalt.Service;
using Asphalt.Service.Config;
using Asphalt.Service.Permissions;
using Asphalt.Service.Settings;
using Eco.Core.Plugins.Interfaces;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Asphalt
{
    public abstract class AsphaltMod : IModKitPlugin, IServerPlugin
    {

        public static string Status { get; protected set; }

        private ServiceManager serviceManager;


        public AsphaltMod()
        {
            Status = "Initializing...";

            try {
                this.OnPreEnable();
            } catch (Exception e) {
                Disable(e, "PreInitialization");
                return;
            }

            //initialize service manager
            this.serviceManager = new ServiceManager(this);
            try {
                this.serviceManager.RegisterServices();
            } catch (Exception e) {
                Disable(e, "Initialization");
                return;
            }

            try {
                this.OnEnable();
            } catch (Exception e) {
                Disable(e, "Initialization");
                return;
            }


            try {
                this.OnPostEnable();
            } catch (Exception e) {
                Disable(e, "PostInitialization");
                return;
            }

            Status = "Complete!";
        }

        public virtual string GetStatus()
        {

            return Status;
        }

        public virtual void OnPreEnable()
        {

        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnPostEnable()
        {

        }

        public void Disable(Exception e, string phase)
        {
            Status = "Diasabled!";

            if (e is TargetInvocationException)
                e = ((TargetInvocationException)e).InnerException;
            //blanks just to prevent overlayed text...
            Log.WriteError("");
            Log.WriteError("Caught exception in "+phase+" phase of "+this.ToString()+"!  Disabling...\n"+e.ToString()+"");
            Log.WriteError("");
            throw e;
#if Debug
            throw e;
#endif
        }

        public virtual List<Type> GetCustomSettings()
        {
            return null;
        }

        public virtual List<Permission> GetPermissions()
        {
            return null;
        }

        public virtual List<ConfigField> GetConfigFields()
        {
            return null;
        }

        public SettingsService GetSettingsService()
        {
            try
            {
                return (SettingsService) this.serviceManager.GetService<SettingsService>();
            }
            catch
            {
                //TODO: Log smth...
            }

            return null;
        }

        public PermissionsService GetPermissionsService()
        {
            try
            {
                return (PermissionsService)this.serviceManager.GetService<PermissionsService>();
            }
            catch
            {
                //TODO: Log smth...
            }

            return null;
        }
    }
}
