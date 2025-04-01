using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.IServices
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllArtistsAsync();
        Task<Artist?> GetArtistByIdAsync(string artistId);
        Task<Artist> CreateArtistAsync(Artist artist);
        Task<Artist?> UpdateArtistAsync(string artistId, Artist updatedArtist);
        Task<bool> DeleteArtistAsync(string artistId);
    }
}
