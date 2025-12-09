using HireAI.Data.Models;
using HireAI.Infrastructure.Context;
using HireAI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Infrastructure.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly HireAIDbContext _context;

        public PaymentMethodRepository(HireAIDbContext context) => _context = context;

        public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }

        //get paymentmethod by id
        public async Task<PaymentMethod?> GetPaymentMethodByIdAsync(int id) => await _context.PaymentMethods.FindAsync(id);

        //get all paymentmethods
        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync() => await _context.PaymentMethods.ToListAsync();

        //delete paymentmethod
        public async Task DeletePaymentMethodAsync(int id)
        {
            var pm = await _context.PaymentMethods.FindAsync(id);
            if (pm != null)
            {
                _context.PaymentMethods.Remove(pm);
                await _context.SaveChangesAsync();
            }
        }
    }
}