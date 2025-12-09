using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string CardholderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; } = string.Empty;
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}