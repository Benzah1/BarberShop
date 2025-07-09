using Application.DTOs;
using Application.Interfaces;
using Application.Exceptions;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly TurnoContext _context;
    private readonly IConfiguration _config;
    private readonly IEmailService _emailService;

    public AuthService(TurnoContext context, IConfiguration config, IEmailService emailService)
    {
        _context = context;
        _config = config;
        _emailService = emailService;
    }

    public async Task RegisterUser(RegisterDTO dto)
    {
        var exists = await _context.Users.AnyAsync(u => u.UserEmail == dto.Email);
        if (exists)
            throw new BadRequestException("Este correo ya está registrado.");

        var verificationCode = Guid.NewGuid().ToString().Substring(0, 6);
        var expiry = DateTime.UtcNow.AddMinutes(15);

        var user = new User
        {
            UserName = dto.Name,
            UserEmail = dto.Email,
            Role = dto.Role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            EmailConfirmed = false,
            EmailVerificationCode = verificationCode,
            EmailVerificationExpiry = expiry
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // 👉 Link que apunta a tu API para verificación
        var verificationLink = $"http://localhost:5010/api/auth/verify?email={dto.Email}&code={verificationCode}";

        var subject = "📩 Verifica tu correo - BarberShop";
        var body = $@"
        <h2>¡Hola {dto.Name}!</h2>
        <p>Gracias por registrarte en <strong>BarberShop</strong>.</p>
        <p>Tu código de verificación es:</p>
        <h3 style='color:#2e86de'>{verificationCode}</h3>
        <p>O haz clic en el siguiente enlace para verificar tu correo:</p>
        <a href='{verificationLink}' style='color: #2e86de'>Verificar correo</a>
        <p>Este código expira en 15 minutos.</p>
        <hr />
        <p>Si no fuiste tú, ignora este mensaje.</p>
        <p style='font-size:small;'>Equipo BarberShop</p>
    ";

        await _emailService.SendEmail(new EmailDTO
        {
            To = dto.Email,
            Subject = subject,
            Body = body
        });
    }

    public async Task<string> LoginUser(LoginDTO dto)
    {

        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserEmail == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.PasswordHash, user.PasswordHash))
            throw new BadRequestException("Correo o contraseña incorrectos.");

        if (!user.EmailConfirmed)
            throw new BadRequestException("Debes verificar tu correo para iniciar sesión.");


        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.UserEmail),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task ConfirmEmail(string email, string code)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);

        if (user == null)
            throw new NotFoundException("Usuario no encontrado.");

        if (user.EmailConfirmed)
            throw new BadRequestException("Este correo ya fue confirmado.");

        if (user.EmailVerificationCode != code)
            throw new BadRequestException("Código incorrecto.");

        if (user.EmailVerificationExpiry < DateTime.UtcNow)
            throw new BadRequestException("El código ha expirado.");

        user.EmailConfirmed = true;
        user.EmailVerificationCode = null;
        user.EmailVerificationExpiry = null;

        await _context.SaveChangesAsync();
    }
}
