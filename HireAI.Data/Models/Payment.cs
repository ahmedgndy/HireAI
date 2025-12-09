using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        // amount >> money
        public decimal Amount { get; set; }
        //usd>>doller 
        public string Currency { get; set; } = "USD";
        public string Description { get; set; } = string.Empty;
        public string StripePaymentIntentId { get; set; } = string.Empty;
        public string Status { get; set; } = "pending";

        // FK to PaymentMethod
        public int PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        //public string PaymentIntentId { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime? UpdatedAt { get; set; }
    }
}