using FoodIntelligence.Data;
using FoodIntelligence.Service;
using FoodIntelligence.Service.Services.RestaurantesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/restaurantes")]
    [ApiController]
    //[Authorize]
    public class RestaurantesController : ControllerBase
    {
        protected readonly IRestaurantesService _service;

        public RestaurantesController(IRestaurantesService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userIdClaim = User.FindFirst("UserId");
            string userId = userIdClaim.Value;
            var uerlist= _service.GetAll(userId);
            return Ok(uerlist);
        }
    }
}
