﻿using Asphalt.Api.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Events.Console
{
    /// <summary>
    /// Called when a line was written to the server console
    /// </summary>
    public class ConsoleLineInputEvent : IEvent
    {
        public string Text { get; protected set; }

        public ConsoleLineInputEvent(string pText)
        {
            Text = pText;
        }
    }
}
