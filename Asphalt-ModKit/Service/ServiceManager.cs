/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 26, 2018
 * ------------------------------------
 **/

using Asphalt.AsphaltExceptions;
using System;
using System.Collections.Generic;

namespace Asphalt.Service
{
    public class ServiceManager
    {
        public AsphaltMod Mod { get; private set; }

        private Dictionary<Type, AbstractService> services = new Dictionary<Type, AbstractService>();

        public ServiceManager(AsphaltMod mod)
        {
            this.Mod = mod;
        }

        public void RegisterServices()
        {
            try
            {
                //register Asphalt services
                //TODO

                //find and register custom services
                //TODO
            } 
            catch(ServiceAlreadyExistingException e)
            {
                throw new ServiceInitException(e.Message);
            }

        }

        public void RegisterService<TService>() where TService : AbstractService
        {
            if (this.Exists<TService>())
                throw new ServiceAlreadyExistingException(typeof(TService).ToString());

            AbstractService service;
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

        public bool Exists<TService>() where TService : AbstractService
        {
            return services.ContainsKey(typeof(TService));
        }

        public AbstractService GetService<TService>() where TService : AbstractService
        {
            if (!this.Exists<TService>())
                return null;

            return this.services[typeof(TService)];
        }

        public void reload<TService>() where TService : AbstractService
        {
            if (!this.Exists<TService>())
                throw new ServiceNotKnownException(typeof(TService).ToString());

            this.services[typeof(TService)].Reload();
        }

        public void reload()
        {
            try
            {
                foreach (KeyValuePair<Type, AbstractService> pair in services)
                    pair.Value.Reload();
            } 
            catch
            {
                throw new ServiceReloadException();
            }
        }
    }
}
