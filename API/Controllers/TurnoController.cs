using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurnosController : ControllerBase
{
    private readonly ITurnoService _turnoService;

    public TurnosController(ITurnoService turnoService)
    {
        _turnoService = turnoService;
    }

    // GET: api/turnos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var turnos = await _turnoService.GetAllTurns();
        return Ok(turnos);
    }

    // GET: api/turnos/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var turno = await _turnoService.GetTurnById(id);
        return Ok(turno); // Si no existe, el service lanza NotFoundException
    }

    // POST: api/turnos
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TurnoDTO dto)
    {
        await _turnoService.CreateTurn(dto);
        return Ok("Turno Creado Exitosamente");
    }

    // PUT: api/turnos/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TurnoDTO dto)
    {
        await _turnoService.UpdateTurn(id, dto);
        return Ok("Su hora se ha actualizado correctamente");
    }

    // DELETE: api/turnos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _turnoService.DeleteTurn(id);
        return Ok("Su hora se ha cancelado correctamente");
    }
}

