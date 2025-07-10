using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        await _authService.RegisterUser(dto);
        return Ok("Usuario registrado correctamente.");
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _authService.LoginUser(dto);
        return Ok(new { token });
    }

    [HttpGet("verify")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string code)
    {
        await _authService.ConfirmEmail(email, code);
        return Ok("✅ ¡Correo verificado correctamente!");
    }

    [HttpPost("resend-verification")]
    public async Task<IActionResult> ResendVerificationCode([FromBody] string email)
    {
        await _authService.ResendVerificationCode(email);
        return Ok("Código reenviado con éxito.");
    }


}

