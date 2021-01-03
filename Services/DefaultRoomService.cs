using AutoMapper;
using LandonHotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonHotelAPI.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelAPIDbContext _context;
        private readonly IMapper _mapper;

        public DefaultRoomService(HotelAPIDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<Room>(entity);
        }
    }
}
