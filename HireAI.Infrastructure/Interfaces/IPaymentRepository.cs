using HireAI.Data.Models;
using HireAI.Infrastructure.GenaricBasies;

namespace HireAI.Infrastructure.Intrefaces
{
    public interface IPaymentRepository
    {
        // Payments
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);

        // Plans
        Task<Plan> CreatePlanAsync(Plan plan);
        Task<Plan?> GetPlanByIdAsync(int id);
        Task<IEnumerable<Plan>> GetAllPlansAsync();
        Task<Plan> UpdatePlanAsync(Plan plan);
        Task DeletePlanAsync(int id);

        // Subscriptions
        Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
        Task<Subscription?> GetSubscriptionByIdAsync(int id);
        Task<Subscription?> GetCurrentSubscriptionAsync();
        Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription> UpdateSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(int id);
    }
}