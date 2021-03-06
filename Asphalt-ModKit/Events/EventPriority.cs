﻿/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 25, 2018
 * ------------------------------------
 **/

namespace Asphalt.Api.Event
{
    /**
     * Events are called from HIGHEST to LOWEST.
     **/
    public enum EventPriority
    {
        Lowest = 0,
        Low,
        Normal,
        High,
        Highest,
        Monitor
    };
}
