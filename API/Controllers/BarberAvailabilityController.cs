using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarberAvailabilityController : ControllerBase
{
    private readonly IBarberAvailabilityService _availabilityService;

    public BarberAvailabilityController(IBarberAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    // POST: api/BarberAvailability
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BarberAvailabilityCreateDTO dto)
    {
        await _availabilityService.CreateAvailabilityRule(dto);
        return Ok("Regla de disponibilidad creada correctamente");
    }

    // GET: api/BarberAvailability/barber/5
    [HttpGet("barber/{barberId}")]
    public async Task<IActionResult> GetByBarber(int barberId)
    {
        var result = await _availabilityService.GetAvailabilityByBarber(barberId);
        return Ok(result);
    }

    // PUT: api/BarberAvailability/3
    [HttpPut("{ruleId}")]
    public async Task<IActionResult> Update(int ruleId, [FromBody] BarberAvailabilityUpdateDTO dto)
    {
        await _availabilityService.UpdateAvailabilityRule(ruleId, dto);
        return Ok("Regla de disponibilidad actualizada correctamente");
    }

    // DELETE: api/BarberAvailability/3
    [HttpDelete("{ruleId}")]
    public async Task<IActionResult> Delete(int ruleId)
    {
        await _availabilityService.DeleteAvailabilityRule(ruleId);
        return Ok("Regla de disponibilidad eliminada correctamente");
    }


    [HttpGet("disponibles")]
    public async Task<IActionResult> GetAvailableSlots([FromQuery]int barberId, [FromQuery] DateTime date, [FromQuery]int serviceId)
    {
        var slots = await _availabilityService.GetAvailableTimeSlots(barberId, date, serviceId);
        return Ok(slots);
    }
}
