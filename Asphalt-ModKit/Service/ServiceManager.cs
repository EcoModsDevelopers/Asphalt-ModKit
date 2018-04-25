/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 26, 2018
 * ------------------------------------
 **/

using Asphalt.AsphaltExceptions;
using Asphalt.Service.Config;
using Asphalt.Service.Permissions;
using Asphalt.Service.Settings;
using System;
using System.Collections.Generic;

namespace Asphalt.Service
{
    public class ServiceManager
    {
        public AsphaltMod Mod { get; private set; }

        private Dictionary<Type, IAspahltService> services = new Dictionary<Type, IAspahltService>();

        public ServiceManager(AsphaltMod mod)
        {
            this.Mod = mod;
        }


        public void RegisterService<TService>() where TService : IAspahltService
        {
            if (this.Exists<TService>())
                throw new ServiceAlreadyExistingException(typeof(TService).ToString());

            IAspahltService service;
            try
            {
                service = (TService)Activator.CreateInstance(typeof(TService), this.Mod);
                service.Init();
                services.Add(typeof(TService), service);
            }
            catch (Exception e)
            {
                throw new ServiceInitException(e.Message);
            }
        }

        public bool Exists<TService>() where TService : IAspahltService
        {
            return services.ContainsKey(typeof(TService));
        }

        public IAspahltService GetService<TService>() where TService : IAspahltService
        {
            if (!this.Exists<TService>())
                return null;

            return this.services[typeof(TService)];
        }

        public void Reload<TService>() where TService : IAspahltService
        {
            if (!this.Exists<TService>())
                throw new ServiceNotKnownException(typeof(TService).ToString());

            this.services[typeof(TService)].Reload();
        }

        public void Reload()
        {
            try
            {
                foreach (KeyValuePair<Type, IAspahltService> pair in services)
                    pair.Value.Reload();
            }
            catch
            {
                throw new ServiceReloadException();
            }
        }
    }
}
