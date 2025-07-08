using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: api/users/{UserId}/profile
        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _userService.GetProfile(id); 
            return Ok(profile);
        }

        // PUT: api/users/{UserId}
        [HttpPut("{id}")]
        public async Task<IActionResult> Updateprofile(int id, [FromBody] UpdateUserDTO dto)
        {
            await _userService.UpdateProfile(id, dto);
            return Ok("Su perfil se ha actualizado correctamente");
        }

        // PUT: api/users/{UserId}/change-password
        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> Updatepassword(int id, [FromBody] ChangePasswordDTO dto)
        {
            await _userService.ChangePassword(id, dto);
            return Ok("Su contraseña se ha actualizado correctamente");
        }

    }
}
