using api.Extenstions;
using Api.Core.Domain;
using Api.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using Api.Core.Dtos.OrderDTOS;
using Api.Core.Domain.Models;

namespace Api.Presentation.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderInterface _orderInterface;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutController(IConfiguration configuration, IOrderInterface orderInterface, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
            {
                _configuration = configuration;
                _orderInterface = orderInterface;
                _userManager = userManager;
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
                _httpContextAccessor = httpContextAccessor;
            }


      [HttpPost("create-payment-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
    {
        string userName = User.GetUserName();
        AppUser ? user = await _userManager.FindByNameAsync(userName);

        Order order =  await _orderInterface.GetOrder(user.Id, request.OrderId);

        if (order == null)
            return BadRequest("Invalid Order ID");
        
        OrderResponseDTO orderResponseDTO = order.ToOrderResponseDTO();

        string clientIpAddress =  _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();


         var customerOptions = new CustomerCreateOptions
        {
            Email = user.Email,         // User's email address
            Name = user.UserName        // User's name
        };

        var customerService = new CustomerService();
        var customer = customerService.Create(customerOptions);

        var options = new PaymentIntentCreateOptions
        {
            Amount = (long?)orderResponseDTO.TotalPrice * 100,
            Currency = request.Currency,
            PaymentMethodTypes = new List<string> { "card" },
            Customer = customer.Id,  

             Metadata = new Dictionary<string, string>
            {
                { "order_id", request.OrderId.ToString() },
                { "user_name", userName },
                { "IP_address", clientIpAddress },
                { "Customer_email", user.Email },
                { "Customer_name", user.UserName }
            },

            
            ReceiptEmail = user.Email,
            Description = $"Payment for order {request.OrderId} by {userName}",

        };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

        
            var confirmOptions = new PaymentIntentConfirmOptions
            {
                PaymentMethod = "pm_card_visa", 
            };

            await service.ConfirmAsync(paymentIntent.Id, confirmOptions);

            return Ok(new { PaymentId = paymentIntent.Id });

     }

     [HttpPost("confirm-payment")]
    public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
    {
        try
        {
            // Retrieve PaymentIntent from Stripe using payment_intent_id
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(request.PaymentIntentId);

            // Check if PaymentIntent exists and is successful
            if (paymentIntent == null)
            {
                return BadRequest("Invalid PaymentIntent ID");
            }

            if (paymentIntent.Status != "succeeded")
            {
                return BadRequest("PaymentIntent has not been successfully processed");
            }

            // Retrieve order information from PaymentIntent metadata
            int orderId;
            if (!int.TryParse(paymentIntent.Metadata["order_id"], out orderId))
            {
                return BadRequest("Invalid order ID in PaymentIntent metadata");
            }

            // Retrieve order and mark it as completed (simulated)
            // Replace with your actual order update logic
            string userName = User.GetUserName();
            AppUser ? user = await _userManager.FindByNameAsync(userName);

            Order order = await _orderInterface.GetOrder(user.Id,orderId);

            if (order == null)
                return BadRequest("Order not found");
        

            // _orderInterface.UpdateOrder(order); // Uncomment and implement your order update logic

            // Return success response
            return Ok(new { Message = "Payment confirmed and order marked as completed" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error confirming payment: {ex.Message}");
        }
    }



    }

}