using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Authentication
{
    public interface IJwtService
    {
       public string GenerateToken(ApplicationUser user);
    }
}
