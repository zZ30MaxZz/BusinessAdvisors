using Mapster;
using Microsoft.AspNetCore.Mvc;
using SoftTeK.BusinessAdvisors.Data.Entities;
using SoftTeK.BusinessAdvisors.Data.Interface;
using SoftTeK.BusinessAdvisors.Dto.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftTeK.BusinessAdvisors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
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
    }
}
