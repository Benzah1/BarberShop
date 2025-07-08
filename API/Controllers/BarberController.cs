using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _barberService;

        public BarberController(IBarberService barberService)
        {
            _barberService = barberService;
        }


        // GET: api/barbers
        [HttpGet]
        public async Task<IActionResult> GetAllBarber()
        {
            var turnos = await _barberService.GetAllBarbers();
            return Ok(turnos);
        }

        // GET: api/barbers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarber(int id)
        {
            var turnos = await _barberService.GetBarberById(id);
            return Ok(turnos);
        }

        // POST: api/barbers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarberDTO dto)
        {
            await _barberService.CreateBarber(dto);
            return Ok("Barbero creado exitosamente");
        }

        // PUT: api/barbers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BarberDTO dto)
        {
            await _barberService.UpdateBarber(id, dto);
            return Ok("El barbero se ha actualizado correctamente");
        }

        // DELETE: api/barbers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _barberService.DeleteBarber(id);
            return Ok("El Barbero se ha borrado correctamente");
        }

    }
}
