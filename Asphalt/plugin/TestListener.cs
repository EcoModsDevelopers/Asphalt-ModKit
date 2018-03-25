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
        [EventHandler("PlayerInteractObjectEvent")]
        public void testEvent(PlayerInteractObjectEvent _event) {
            Console.WriteLine("PlayerInteractObjectEvent triggered");
        }

        [EventHandler("Test2")]
        public void test2Event(PlayerInteractObjectEvent _event)
        {
            Console.WriteLine("Test2 triggered");
        }
    }
}
