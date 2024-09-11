using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIRabbitMQPublisher.DTOs;
using WebAPIRabbitMQPublisher.Services;

namespace WebAPIRabbitMQPublisher.Controllers
{
    [ApiController]
    [Route("pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly RabbitMQProducer messagePublisher;

        public PedidosController(RabbitMQProducer messagePublisher)
        {
            this.messagePublisher = messagePublisher;
        }

        [HttpPost]
        public ActionResult<DTOPedido> PostPedido(DTOPedido pedido)
        {
            DTOPedido newPedido = new DTOPedido()
            {
                Nombre = pedido.Nombre,
                Precio = pedido.Precio,
                Cantidad = pedido.Cantidad
            };

            messagePublisher.SendMessage(newPedido);

            return Ok();
        }
    }

}
