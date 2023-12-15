using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Service.Services.DetallePedidoServices;
using FoodIntelligence.Service.Services.PedidosServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/DetallesPedidos")]
    [ApiController]
    [Authorize]
    public class DetallesPedidosController : ControllerBase
    {
        protected readonly IDetallePedidoService _service;

        public DetallesPedidosController(IDetallePedidoService service)
        {
            _service = service;
        }
        [HttpPut]
        public async Task<IActionResult> Get(int id)
        {
            var entityList = _service.Delete(id);
            return Ok(entityList);
        }

    }
}
