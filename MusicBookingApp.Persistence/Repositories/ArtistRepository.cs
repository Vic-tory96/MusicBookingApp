using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Persistence.DBContext;

namespace MusicBookingApp.Persistence.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private readonly MusicBookingDbContext _context;

        public ArtistRepository(MusicBookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Artist?> GetArtistByStageNameAsync(string stageName)
        {
            return await _context.Artists.FirstOrDefaultAsync(a => a.Name == stageName);
        }
    }
}
