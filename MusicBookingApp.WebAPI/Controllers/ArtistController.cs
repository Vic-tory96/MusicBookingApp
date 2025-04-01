using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Application.Response;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;
        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            var artistDtos = _mapper.Map<IEnumerable<ArtistDto>>(artists);
            return Ok(new ApiResponse<IEnumerable<ArtistDto>>(true, 200, "Artists retrieved successfully.", artistDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(string id)
        {
            var artist = await _artistService.GetArtistByIdAsync(id);
            if (artist == null)
                return NotFound(new ApiResponse<ArtistDto>(false, 404, "Artist not found.", null));

            var artistDto = _mapper.Map<ArtistDto>(artist);
            return Ok(new ApiResponse<ArtistDto>(true, 200, "Artist retrieved successfully.", artistDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);
            var createdArtist = await _artistService.CreateArtistAsync(artist);
            var createdArtistDto = _mapper.Map<ArtistDto>(createdArtist);
            return CreatedAtAction(nameof(GetArtistById), new { id = createdArtist.Id }, new ApiResponse<ArtistDto>(true, 201, "Artist created successfully.", createdArtistDto));
        }


        [HttpPut("{artistId}")]
        public async Task<IActionResult> UpdateArtist(string id, [FromBody] ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);
            var updatedArtist = await _artistService.UpdateArtistAsync(id, artist);
            if (updatedArtist == null)
                return NotFound(new ApiResponse<ArtistDto>(false, 404, "Artist not found.", null));

            var updatedArtistDto = _mapper.Map<ArtistDto>(updatedArtist);
            return Ok(new ApiResponse<ArtistDto>(true, 200, "Artist updated successfully.", updatedArtistDto));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(string id)
        {
            var deleted = await _artistService.DeleteArtistAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<bool>(false, 404, "Artist not found.", false));

            return Ok(new ApiResponse<bool>(true, 200, "Artist deleted successfully.", true));
        }
    }
}


