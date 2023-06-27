using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Role_Playing_Game_API.Dtos.User;
using Role_Playing_Game_API.InterFaces;
using Role_Playing_Game_API.Models;

namespace Role_Playing_Game_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepository.Register(
                new User { Username = request.Username}, request.Password
                 );

            if (!response.Success)
            {  
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
