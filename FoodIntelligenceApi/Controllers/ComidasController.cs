using FoodIntelligence.Data;
using FoodIntelligence.Service;
using FoodIntelligence.Service.Services.ComidasServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/Comidas")]
    [ApiController]
    [Authorize]
    public class ComidasController : ControllerBase
    {
        protected readonly IComidasService _service;

        public ComidasController(IComidasService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int idRestaurante)
        {
            var userIdClaim = User.FindFirst("UserId");
            string userId = userIdClaim.Value;
            var entityList = _service.GetAll(idRestaurante, userId);
            return Ok(entityList);
        }
    }
}
