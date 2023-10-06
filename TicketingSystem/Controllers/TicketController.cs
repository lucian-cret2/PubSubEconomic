using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Domain;
using TicketingSystem.Services;

namespace TicketingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> InitiatePurchase(Purchase purchase)
        {

            await _ticketService.InitiatePurchase(purchase);

            return Ok();
        }
    }
}
