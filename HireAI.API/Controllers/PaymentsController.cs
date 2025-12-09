using HireAI.Data.Models;
using HireAI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace HireAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _svc;

        public PaymentsController(IPaymentService svc) => _svc = svc;

        // Post Payments 
        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody] CreatePaymentRequest req)
        {
            var p = await _svc.ProcessPaymentAsync(req);
            return Ok(new { success = true, payment = p });
        }
        //Get Payment
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _svc.GetPaymentByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        //Get all Pyament
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllPaymentsAsync());

        //update payment 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Payment payment)
        {
            if (id != payment.Id) return BadRequest();
            var updated = await _svc.UpdatePaymentAsync(payment);
            return Ok(updated);
        }

        //delete payment

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _svc.DeletePaymentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // Payment methods
        [HttpPost("methods")]
        public async Task<IActionResult> CreateMethod([FromBody] PaymentMethod pm) => Ok(await _svc.CreatePaymentMethodAsync(pm));

        //Get paymentmethod 
        [HttpGet("methods/{id}")]
        public async Task<IActionResult> GetMethod(int id)
        {
            var pm = await _svc.GetPaymentMethodByIdAsync(id);
            if (pm == null) return NotFound();
            return Ok(pm);
        }

        //get all methods
        [HttpGet("methods")]
        public async Task<IActionResult> GetAllMethods() => Ok(await _svc.GetAllPaymentMethodsAsync());


        //delete all methods
        [HttpDelete("methods/{id}")]
        public async Task<IActionResult> DeleteMethod(int id)
        {
            await _svc.DeletePaymentMethodAsync(id);
            return NoContent();
        }

        // get Plans
        [HttpGet("plans")]
        public async Task<IActionResult> GetPlans() => Ok(await _svc.GetAllPlansAsync());

        //get plan id 
        [HttpGet("plans/{id}")]
        public async Task<IActionResult> GetPlan(int id)
        {
            await _svc.GetPlanByIdAsync(id);
            return NoContent();
        }


        //plan created heree
        [HttpPost("plans")]
        public async Task<IActionResult> CreatePlan([FromBody] Plan plan) => Ok(await _svc.CreatePlanAsync(plan));

        //update plan
        [HttpPut("plans/{id}")]
        public async Task<IActionResult> UpdatePlan(int id, [FromBody] Plan plan)
        {
            if (id != plan.Id) return BadRequest();
            return Ok(await _svc.UpdatePlanAsync(plan));
        }


        //delete paln
        [HttpDelete("plans/{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            await _svc.DeletePlanAsync(id);
            return NoContent();
        }

        // get Subscriptions
        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetSubscriptions() => Ok(await _svc.GetAllSubscriptionsAsync());

        [HttpGet("subscriptions/current")]
        public async Task<IActionResult> GetCurrentSubscription()
        {
            var s = await _svc.GetCurrentSubscriptionAsync();
            if (s == null) return NotFound();
            return Ok(s);
        }

        //get create subscription
        [HttpPost("subscriptions")]
        public async Task<IActionResult> CreateSubscription([FromBody] Subscription subscription) => Ok(await _svc.CreateSubscriptionAsync(subscription));

        //update plan 
        [HttpPut("subscriptions/{id}")]
        public async Task<IActionResult> UpdateSubscription(int id, [FromBody] Subscription subscription)
        {
            if (id != subscription.Id) return BadRequest();
            return Ok(await _svc.UpdateSubscriptionAsync(subscription));
        }


        //delete plan
        [HttpDelete("subscriptions/{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            await _svc.DeleteSubscriptionAsync(id);
            return NoContent();
        }
    }
}
