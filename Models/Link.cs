using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LandonHotelAPI.Models
{
    
    public class Link
        
    {
        public const string GetMethod = "GET";

        public static Link To(string routeName, object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Realations = null
            };

        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        [JsonProperty(Order = -3,
            PropertyName = "rel",
            NullValueHandling = NullValueHandling.Ignore)]
        public string[] Realations { get; set; }
        [JsonProperty(Order = -2
            DefaultValueHandling= DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; }

        //Stores Route Name before being rewritten by the LinkRewriting Filter
        [JsonIgnore]
        public string RouteName { get; set; }


        //Stores Route Parms before being rewritten by the LinkRewriting Filter
        [JsonIgnore]
         public object RouteValues { get; set; }
    }
}
