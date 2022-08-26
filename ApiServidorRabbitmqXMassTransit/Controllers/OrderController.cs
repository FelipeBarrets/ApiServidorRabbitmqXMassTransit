using ApiServidorRabbitmqXMassTransit.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ApiServidorRabbitmqXMassTransit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IBus _bus;
        public OrderController (IBus bus) { _bus = bus; }

        [HttpPost]
        public async Task<IActionResult> createObj(Dados dados)
        {
            if (dados != null) {
                try {
                    dados.Username = "teste";
                    dados.Location = "teste";
                    dados.Booked = Convert.ToDateTime("2022-08-23");
                    Uri uri = new Uri("rabbitmq://localhost/orderDados");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send(dados);
                    return Ok();
                }
                catch (Exception re) { 
                return BadRequest();
                }
            
            }
            return BadRequest();

        }
    }
}
