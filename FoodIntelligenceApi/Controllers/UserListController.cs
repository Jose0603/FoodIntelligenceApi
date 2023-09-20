using FoodIntelligence.Data;
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
        private readonly IUnitOfWork _unitOfWork;
        public UserListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var uerlist= _unitOfWork.CategoriasComidaRepository.GetAll();
            return Ok(uerlist);
        }
    }
}
