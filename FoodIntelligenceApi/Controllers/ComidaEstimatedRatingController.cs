using FoodIntelligence.Data;
using FoodIntelligence.Service;
using FoodIntelligence.Service.Services.ComidaEstimatedRatingServices;
using FoodIntelligence.Service.Services.ComidasServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/ComidaEstimatedRating")]
    [ApiController]
    public class ComidaEstimatedRatingController : ControllerBase
    {
        protected readonly IComidaEstimatedRatingService _service;

        public ComidaEstimatedRatingController(IComidaEstimatedRatingService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entityList= _service.CreateAll();
            return Ok(entityList);
        }
    }
}
