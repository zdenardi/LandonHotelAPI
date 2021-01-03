using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonHotelAPI.Models
{
    public class RootResponse
    {
        public string Href { get; set; }
        public object Rooms { get; set;t }
        public object Info { get; set; }
    }
}
