﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonHotelAPI.Models
{
    public abstract class Resource
    {
        [JsonProperty(Order= -2)]
        public string Href { get; set; }
    }
}
