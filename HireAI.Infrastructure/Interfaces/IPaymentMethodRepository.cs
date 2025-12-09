using HireAI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Infrastructure.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task<PaymentMethod?> GetPaymentMethodByIdAsync(int id);
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task DeletePaymentMethodAsync(int id);
    }
}