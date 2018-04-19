/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 26, 2018
 * ------------------------------------
 **/

namespace Asphalt.Service
{
    public abstract class AbstractService
    {
        public AsphaltMod Mod { get; private set; }

        public AbstractService(AsphaltMod mod)
        {
            this.Mod = mod;
        }

        public abstract void Init();

        public abstract void Reload();
    }
}
