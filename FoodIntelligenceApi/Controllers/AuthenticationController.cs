using FoodIntelligence.Data.Autentication;
using FoodIntelligence.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var result = await _authService.Login(model);
                if (result.Item1 == 0)
                    return BadRequest(result.Item2);
                return Ok(result.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var result = await _authService.Registeration(model, UserRoles.Admin);
                if (result.Item1 == 0)
                {
                    return BadRequest(result.Item2);
                }
                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("sendVerificationCode")]
        public async Task<IActionResult> SendVerificationCode(string email)
        {
            try
            {
                var result = await _authService.SendVerificationCode(email);
                if (result.Item1 == 0)
                    return BadRequest(result);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("verifyCode")]
        public async Task<IActionResult> VerifyCode(string code)
        {
            try
            {
                var result = await _authService.VerifyCode(code);
                if (result.Item1 == 0)
                    return BadRequest(result.Item2);
                return Ok(result.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            try
            {
                var result = await _authService.ResetPassword(model);
                if (result.Item1 == 0)
                    return BadRequest(result.Item2);
                return Ok(result.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}