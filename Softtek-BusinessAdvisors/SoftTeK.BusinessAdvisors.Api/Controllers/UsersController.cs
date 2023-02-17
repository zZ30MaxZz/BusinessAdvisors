using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftTeK.BusinessAdvisors.Api.Helpers;
using SoftTeK.BusinessAdvisors.Data.Entities;
using SoftTeK.BusinessAdvisors.Data.Interface;
using SoftTeK.BusinessAdvisors.Dto.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftTeK.BusinessAdvisors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IBuildTokenJwt _buildTokenJwt;

        public UsersController(IUserService userService, IBuildTokenJwt buildTokenJwt)
        {
            _userService = userService;
            _buildTokenJwt = buildTokenJwt;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users.Adapt<List<UserDto>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(UserDto model)
        {
            _userService.Create(model.Adapt<User>());
            return Ok(new { message = "Usuario creado correctamente" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UserDto model)
        {
            _userService.Update(id, model.Adapt<User>());
            return Ok(new { message = "Usuario actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "Usuario eliminado" });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserLoginDto model)
        {
            var user = _userService.GetByEmailAndPassword(model.Email!, model.Password!);

            if (user != null)
            {
                var userDto = user.Adapt<UserDto>();

                return Ok(_buildTokenJwt.BuildJwt(userDto));
            }
            else
            {
                return BadRequest("Intento de login fallido.");
            }
        }
    }
}
