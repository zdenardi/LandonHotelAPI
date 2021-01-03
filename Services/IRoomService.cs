using LandonHotelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonHotelAPI.Services
{
    public interface IRoomService
    {
        Task<Room> GetRoomAsync(Guid Id);
    }
}
