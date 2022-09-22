using ApiServidorRabbitmqXMassTransit.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace ApiServidorRabbitmqXMassTransit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IBus _bus;
        public OrderController (IBus bus) { _bus = bus; }   


        [HttpPost]
        public async Task<IActionResult> pagamento([FromBody] Dados dados)
        {
            if (dados != null)
            {
                try
                {
                    Uri uri = new Uri("rabbitmq://localhost/orderDados?bind=true");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send(dados);
                    return Ok();
                }
                catch (Exception e) { BadRequest(); }
            }
            return BadRequest();
        }

    }
}
