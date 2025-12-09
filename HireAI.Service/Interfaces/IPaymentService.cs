using HireAI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Service.Interfaces
{
    public interface IPaymentService
    {
        // Payments
        Task<Payment> ProcessPaymentAsync(CreatePaymentRequest request);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int paymentId);

        // Payment methods
        Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task<PaymentMethod?> GetPaymentMethodByIdAsync(int id);
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task DeletePaymentMethodAsync(int id);

        // Plans
        Task<Plan> CreatePlanAsync(Plan plan);
        Task<Plan?> GetPlanBySlugAsync(string slug);
        Task<IEnumerable<Plan>> GetAllPlansAsync();
        Task<Plan> UpdatePlanAsync(Plan plan);
        Task DeletePlanAsync(string slug);

        // Subscriptions
        Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
        Task<Subscription?> GetSubscriptionByIdAsync(int id);
        Task<Subscription?> GetCurrentSubscriptionAsync();
        Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription> UpdateSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(int id);
        Task GetPlanByIdAsync(int id);
        Task DeletePlanAsync(int id);
    }

    public class CreatePaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Description { get; set; } = string.Empty;
        public string CardholderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; } = string.Empty;
    }
}