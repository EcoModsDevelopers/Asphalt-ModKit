/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

using Asphalt.api.Event;
using Asphalt.api.Event.player;
using System;

namespace Asphalt.plugin
{
    public class TestListener : IListener
    {
        [EventHandler()]
        public void testEvent(PlayerInteractObjectEvent _event) {
            Console.WriteLine("PlayerInteractObjectEvent 1 triggered");
        }

        [EventHandler(EventPriority.HIGH)]
        public void test2Event(PlayerInteractObjectEvent _event)
        {
            Console.WriteLine("PlayerInteractObjectEvent 2 triggered");
            _event.SetCancelled(true);
        }

        [EventHandler(EventPriority.HIGHEST)]
        public void test3Event(PlayerInteractObjectEvent _event)
        {
            Console.WriteLine("PlayerInteractObjectEvent 3 triggered");
        }

        [EventHandler()]
        public void test4Event(PlayerSendMessageEvent _event)
        {
            Console.WriteLine("PlayerSendMessageEvent 1 triggered");
        }
    }
}
