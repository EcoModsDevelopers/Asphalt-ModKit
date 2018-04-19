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
    public interface IConfirmable
    {
        void Call();

        void Abort();

        void Invalidate();
    }
}
