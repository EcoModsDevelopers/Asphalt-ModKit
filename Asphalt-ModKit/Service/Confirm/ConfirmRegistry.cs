/** 
* ------------------------------------
* Copyright (c) 2018 [Kronox]
* See LICENSE file in the project root for full license information.
* ------------------------------------
* Created by Kronox on March 27, 2018
* ------------------------------------
**/

using System.Collections.Generic;
using Eco.Gameplay.Players;

namespace Asphalt.Service.Confirm
{
    class ConfirmRegistry
    {
        public static Dictionary<User, IConfirmable> confirm = new Dictionary<User, IConfirmable>();

        private static class InstanceHolder {
           public static ConfirmRegistry INSTANCE = new ConfirmRegistry();
        }

        private ConfirmRegistry() {}

        public static ConfirmRegistry GetInstance()
        {
            return InstanceHolder.INSTANCE;
        }

        public void Put(User user, IConfirmable command)
        {
            IConfirmable confirmable;

            if (confirm.TryGetValue(user, out confirmable)) {
                confirmable.Invalidate();
                confirm.Remove(user);
            }

            confirm.Add(user, command);
        }

        public bool Has(User user)
        {
            return confirm.ContainsKey(user);
        }

        public IConfirmable Get(User user)
        {
            IConfirmable confirmable;
            return confirm.TryGetValue(user, out confirmable) ? confirmable : null;
        }

        public void Remove(User user)
        {
            confirm.Remove(user);
        }
    }
}
