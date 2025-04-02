using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Persistence.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookingRepository _bookingRepository;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, IBookingRepository bookingRepository)
        {
            _userManager = userManager;
            _bookingRepository = bookingRepository;
        }
        public async Task<ApplicationUser> CreateUserAsync(RegisterUserDto userDto)
        {
            var user = new ApplicationUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return user;
        }

        public async Task<ICollection<Booking>> GetUserBookingsAsync(string userId)
        {
           
            return await _bookingRepository.GetBookingsByUserIdAsync(userId);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> UpdateUserProfileAsync(string userId, string firstName, string lastName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }

            throw new Exception("Failed to update user profile.");
        }
    }
}
