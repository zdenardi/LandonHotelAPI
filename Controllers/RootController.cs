using LandonHotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonHotelAPI.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RootController : ControllerBase
    {
        [HttpGet (Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            
            var response = new RootResponse
            {
                Href = null, //TODO Url.Link(GetRoot), null

                Rooms = Link.To(nameof(RoomsController.GetRooms),null),
                
                Info = Link.To(nameof(InfoController.GetInfo), null),
                
             };
            return Ok(response);
        }

    }
}

