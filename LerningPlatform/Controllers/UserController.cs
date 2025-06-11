using BL.Api;
using BL.Models;
using Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LerningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IBLUser user;
        private readonly ILogger<UserController> _logger;

        public UserController(IBL bl , ILogger<UserController> logger)
        {
            user = bl.BLUser;
            _logger = logger;

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            try
            {
                var userData = await user.Login(login.Name, login.Phone); 
                if (userData == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                return Ok(new LoginResponse { User = userData, Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("SignUp")]
        public ActionResult SignUp([FromBody] User item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Phone) ||
                    item.Phone.Length != 10 ||
                    !item.Phone.All(char.IsDigit))
                {
                    return BadRequest("Phone must be a string of 10 digits.");
                }
                user.SignUp(item);
                var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while signing up the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

    }
}
