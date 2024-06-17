using api.Extenstions;
using Api.Core.Domain;
using Api.Core.Domain.Models;
using Api.Core.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using api.Extenstions;
using Api.Core.Models;
using Api.Core.Dtos.OrderItemDTO;
using Api.Core.Domain;

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

    private static string s_wasmClientURL = string.Empty;

        public CheckoutController(IConfiguration configuration, IOrderInterface orderInterface, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _orderInterface = orderInterface;
            _userManager = userManager;
        }

        [HttpPost]
    public async Task<ActionResult> CheckoutOrder([FromQuery] int orderId, [FromServices] IServiceProvider sp)
    {
        var referer = Request.Headers.Referer;
        s_wasmClientURL = referer[0];

        // Build the URL to which the customer will be redirected after paying.
        var server = sp.GetRequiredService<IServer>();

        var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

        string? thisApiUrl = null;

        string userName = User.GetUserName();
        AppUser ? user = await _userManager.FindByNameAsync(userName);
        
        Order ? order = await _orderInterface.GetOrder(user.Id, orderId);

        if (serverAddressesFeature is not null)
        {
            thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
        }

        if (thisApiUrl is not null)
        {
            var sessionId = await CheckOut(order, thisApiUrl);
            var pubKey = _configuration["Stripe:PubKey"];

            var checkoutOrderResponse = new CheckOutOrderResponse()
            {
                SessionId = sessionId,
                PubKey = pubKey
            };

            return Ok(checkoutOrderResponse);
        }
        else
        {
            return StatusCode(500);
        }
    }

    [NonAction]
    public async Task<string> CheckOut(Order order, string thisApiUrl)
    {
        // Create a payment flow from the items in the cart.
        // Gets sent to Stripe API.
        var lineItems = new List<SessionLineItemOptions>();

        foreach (var item in order.orderItems)
        {
            var product = item.Product; // Assuming orderItems contains Product reference or data
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long?)(item.Quantity * product.Price * 100), // Convert to cents if Price is in USD.
                    Currency = "USD",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Name,
                        Description = product.Description,
                    }
                },
                Quantity = item.Quantity
            });
        }

        var options = new SessionCreateOptions
        {
            // Stripe calls the URLs below when certain checkout events happen such as success and failure.
            SuccessUrl = $"{thisApiUrl}/checkout/success?sessionId=" + "{CHECKOUT_SESSION_ID}", // Customer paid.
            CancelUrl = s_wasmClientURL + "failed",  // Checkout cancelled.
            PaymentMethodTypes = new List<string> // Only card available in test mode?
            {
                "card"
            },
            LineItems = lineItems,
            Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Id;
    }

    [HttpGet("success")]
    // Automatic query parameter handling from ASP.NET.
    // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
    public ActionResult CheckoutSuccess(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        // Here you can save order and customer details to your database.
        var total = session.AmountTotal.Value;
        var customerEmail = session.CustomerDetails.Email;

        return Redirect(s_wasmClientURL + "success");
    }
}
}