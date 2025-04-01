using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBookingApp.Application.IServices
{
    public interface IFlutterwaveService
    {
        Task<string> InitializePaymentAsync(decimal amount, string email, string txRef);
        Task<bool> VerifyPaymentAsync(string transactionId);
    }
}
