using HireAI.Data.Models;
using HireAI.Infrastructure.Interfaces;
using HireAI.Infrastructure.Intrefaces;
using HireAI.Service.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentService(IPaymentRepository paymentRepository, IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentRepository = paymentRepository;
            _paymentMethodRepository = paymentMethodRepository;
        }

        // Payments
        public async Task<Payment> ProcessPaymentAsync(CreatePaymentRequest request)
        {
            var pm = new Data.Models.PaymentMethod
            {
                CardholderName = request.CardholderName,
                CardNumber = MaskCardNumber(request.CardNumber),
                ExpiryMonth = request.ExpiryMonth,
                ExpiryYear = request.ExpiryYear,
                CVV = request.CVV
            };

            var savedPm = await _paymentMethodRepository.CreatePaymentMethodAsync(pm);

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(request.Amount * 100),
                Currency = request.Currency.ToLower(),
                Description = request.Description,
                ConfirmationMethod = "automatic",
                Confirm = true
            };

            var intentService = new PaymentIntentService();
            PaymentIntent? paymentIntent = null;

            try
            {
                paymentIntent = await intentService.CreateAsync(options);
            }
            catch (StripeException ex)
            {
                var failed = new Payment
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    Description = request.Description,
                    PaymentMethodId = savedPm.Id,
                    Status = "failed",
                    StripePaymentIntentId = ex.Message
                    //CreatedAt = DateTime.UtcNow
                };
                return await _paymentRepository.CreatePaymentAsync(failed);
            }

            var payment = new Payment
            {
                Amount = request.Amount,
                Currency = request.Currency,
                Description = request.Description,
                PaymentMethodId = savedPm.Id,
                Status = paymentIntent?.Status ?? "unknown",
                StripePaymentIntentId = paymentIntent?.Id ?? string.Empty
                //CreatedAt = DateTime.UtcNow,
                //UpdatedAt = DateTime.UtcNow
            };

            return await _paymentRepository.CreatePaymentAsync(payment);
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id) => await _paymentRepository.GetPaymentByIdAsync(id);
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync() => await _paymentRepository.GetAllPaymentsAsync();
        public async Task<Payment> UpdatePaymentAsync(Payment payment) => await _paymentRepository.UpdatePaymentAsync(payment);
        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            var existing = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (existing == null) return false;
            await _paymentRepository.DeletePaymentAsync(paymentId);
            return true;
        }

        // Payment methods
        public async Task<Data.Models.PaymentMethod> CreatePaymentMethodAsync(Data.Models.PaymentMethod paymentMethod) => await _paymentMethodRepository.CreatePaymentMethodAsync(paymentMethod);
        public async Task<Data.Models.PaymentMethod?> GetPaymentMethodByIdAsync(int id) => await _paymentMethodRepository.GetPaymentMethodByIdAsync(id);
        public async Task<IEnumerable<Data.Models.PaymentMethod>> GetAllPaymentMethodsAsync() => await _paymentMethodRepository.GetAllPaymentMethodsAsync();
        public async Task DeletePaymentMethodAsync(int id) => await _paymentMethodRepository.DeletePaymentMethodAsync(id);

        // Plans
        public async Task<Data.Models.Plan> CreatePlanAsync(Data.Models.Plan plan) => await _paymentRepository.CreatePlanAsync(plan);
        public async Task<Data.Models.Plan?> GetPlanByIdAsync(int id) => await _paymentRepository.GetPlanByIdAsync(id);
        public async Task<IEnumerable<Data.Models.Plan>> GetAllPlansAsync() => await _paymentRepository.GetAllPlansAsync();
        public async Task<Data.Models.Plan> UpdatePlanAsync(Data.Models.Plan plan) => await _paymentRepository.UpdatePlanAsync(plan);
        public async Task DeletePlanAsync(int id) => await _paymentRepository.DeletePlanAsync(id);

        // Subscriptions
        public async Task<Data.Models.Subscription> CreateSubscriptionAsync(Data.Models.Subscription subscription) => await _paymentRepository.CreateSubscriptionAsync(subscription);
        public async Task<Data.Models.Subscription?> GetSubscriptionByIdAsync(int id) => await _paymentRepository.GetSubscriptionByIdAsync(id);
        public async Task<Data.Models.Subscription?> GetCurrentSubscriptionAsync() => await _paymentRepository.GetCurrentSubscriptionAsync();
        public async Task<IEnumerable<Data.Models.Subscription>> GetAllSubscriptionsAsync() => await _paymentRepository.GetAllSubscriptionsAsync();
        public async Task<Data.Models.Subscription> UpdateSubscriptionAsync(Data.Models.Subscription subscription) => await _paymentRepository.UpdateSubscriptionAsync(subscription);
        public async Task DeleteSubscriptionAsync(int id) => await _paymentRepository.DeleteSubscriptionAsync(id);

        private string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 8) return cardNumber;
            return cardNumber.Substring(0, 4) + new string('*', cardNumber.Length - 8) + cardNumber.Substring(cardNumber.Length - 4);
        }

        public Task<Data.Models.Plan?> GetPlanBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlanAsync(string slug)
        {
            throw new NotImplementedException();
        }

        Task IPaymentService.GetPlanByIdAsync(int id)
        {
            return GetPlanByIdAsync(id);
        }
    }
}