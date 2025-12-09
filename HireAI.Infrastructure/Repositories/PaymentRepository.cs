using HireAI.Data.Models;
using HireAI.Infrastructure.Context;
using HireAI.Infrastructure.GenaricBasies;
using HireAI.Infrastructure.Intrefaces;
using Microsoft.EntityFrameworkCore;

namespace HireAI.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HireAIDbContext _context;

        public PaymentRepository(HireAIDbContext context) => _context = context;

        // Payments
        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        //get payment by id 
        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.PaymentMethod)
                //return element if id ==id
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //get ll payment 
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                //include> to load data relation
                .Include(p => p.PaymentMethod)
                //convert resut to list
                .ToListAsync();
        }

        //update payment
        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        //delete payment
        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        // Plans created
        public async Task<Plan> CreatePlanAsync(Plan plan)
        {
            // Ignore any incoming explicit identity value so the DB generates it.
            plan.Id = 0;

            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        //get plan id
        public async Task<Plan?> GetPlanByIdAsync(int id) => await _context.Plans.FindAsync(id);

        //get all plan 
        public async Task<IEnumerable<Plan>> GetAllPlansAsync() => await _context.Plans.ToListAsync();

        //update plan
        public async Task<Plan> UpdatePlanAsync(Plan plan)
        {
            _context.Plans.Update(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        //delete all plan
        public async Task DeletePlanAsync(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }

        // Subscriptions
        public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
        {
            // Ignore explicit subscription id
            subscription.Id = 0;

            // If client provided a nested Plan with only Id, load the tracked Plan from DB and attach it.
            if (subscription.Plan != null && subscription.Plan.Id != 0)
            {
                var trackedPlan = await _context.Plans.FindAsync(subscription.Plan.Id);
                if (trackedPlan != null)
                {
                    subscription.Plan = trackedPlan;
                    subscription.PlanTitle = trackedPlan.Title;
                }
                else
                {
                    // Plan id was provided but not found — you can either throw or clear the navigation.
                    // Here we clear the navigation to avoid EF trying to insert the provided Plan.
                    subscription.Plan = null;
                }
            }

            // Ensure we don't accidentally try to insert nested Plan data as new entity
            // (subscription.Plan is either null or a tracked Plan).
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        //get subscription by id
        public async Task<Subscription?> GetSubscriptionByIdAsync(int id)
        {
            return await _context.Subscriptions
                                 .Include(s => s.Plan)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        //get subscription
        public async Task<Subscription?> GetCurrentSubscriptionAsync()
        {
            return await _context.Subscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.Status == "Active");
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync() => await _context.Subscriptions.Include(s => s.Plan).ToListAsync();

        //update subscription
        public async Task<Subscription> UpdateSubscriptionAsync(Subscription subscription)
        {
            // If a nested Plan with Id is provided on update, attach the tracked plan instead of inserting.
            if (subscription.Plan != null && subscription.Plan.Id != 0)
            {
                var trackedPlan = await _context.Plans.FindAsync(subscription.Plan.Id);
                subscription.Plan = trackedPlan; // could be null if not found
                if (trackedPlan != null)
                    subscription.PlanTitle = trackedPlan.Title;
            }

            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        //delete subscription
        public async Task DeleteSubscriptionAsync(int id)
        {
            var sub = await _context.Subscriptions.FindAsync(id);
            if (sub != null)
            {
                _context.Subscriptions.Remove(sub);
                await _context.SaveChangesAsync();
            }
        }
    }
}