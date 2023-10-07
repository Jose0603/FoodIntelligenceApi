using FoodIntelligence.Data;
using FoodIntelligence.Service;
using FoodIntelligence.Service.CategoriasComidaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/userlist")]
    [ApiController]
    [Authorize]
    public class UserListController : ControllerBase
    {
        protected readonly ICategoriasComidaService _service;

        public UserListController(ICategoriasComidaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var uerlist= _service.GetAll();
            return Ok(uerlist);
        }
    }
}
