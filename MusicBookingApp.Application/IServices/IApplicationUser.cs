using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.IServices
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser> CreateUserAsync(RegisterUserDto userDto);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> UpdateUserProfileAsync(string userId, string firstName, string lastName);
        Task<ICollection<Booking>> GetUserBookingsAsync(string userId);
    }
}
