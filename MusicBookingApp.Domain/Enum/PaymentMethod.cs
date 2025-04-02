using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBookingApp.Domain.Enum
{
    public enum PaymentMethod
    {
        CreditCard = 0,
        DebitCard = 1,
        Flutterwave = 2,
        BankTransfer = 3,
        Cash = 4
    }
}
