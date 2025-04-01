using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Interfaces
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist?> GetArtistByStageNameAsync(string stageName);
    }
}
