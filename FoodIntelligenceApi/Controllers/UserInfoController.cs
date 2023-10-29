using FoodIntelligence.Data;
using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Service;
using FoodIntelligence.Service.CategoriasComidaServices;
using FoodIntelligence.Service.Services.CategoriasComidaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        protected readonly IUserInfoService _service;

        public UserInfoController(IUserInfoService service)
        {
            _service = service;
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserDto toEdit)
        {
            var response = await _service.Update(toEdit);
            return Ok(response);
        }
    }
}
