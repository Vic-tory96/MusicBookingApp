using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Persistence.Services
{

    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await _artistRepository.GetAllAsync();
        }

        public async Task<Artist?> GetArtistByIdAsync(string artistId)
        {
            return await _artistRepository.GetByIdAsync(artistId);
        }

        public async Task<Artist> CreateArtistAsync(Artist artist)
        {
            return await _artistRepository.AddAsync(artist);
        }

        public async Task<Artist?> UpdateArtistAsync(string artistId, Artist updatedArtist)
        {
            var existingArtist = await _artistRepository.GetByIdAsync(artistId);
            if (existingArtist == null) return null;

            existingArtist.Name = updatedArtist.Name;
            existingArtist.Genre = updatedArtist.Genre;
            // Add other fields to update

            await _artistRepository.UpdateAsync(existingArtist);
            return existingArtist;
        }

        public async Task<bool> DeleteArtistAsync(string artistId)
        {
            var artist = await _artistRepository.GetByIdAsync(artistId);
            if (artist == null) return false;

            await _artistRepository.DeleteAsync(artist);
            return true;
        }
    }
}


