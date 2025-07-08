using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: api/service/barber/3
        [HttpGet("barber/{barberId}")]
        public async Task<IActionResult> GetByBarber(int barberId)
        {
            var servicios = await _serviceService.GetServicesByBarber(barberId);
            return Ok(servicios);
        }

        // POST: api/service
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDTO dto)
        {
            await _serviceService.CreateService(dto);
            return Ok("Servicio creado correctamente");
        }

        // PUT: api/service/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceDTO dto)
        {
            await _serviceService.UpdateService(id, dto);
            return Ok("Servicio actualizado correctamente");
        }

        // DELETE: api/service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteService(id);
            return Ok("Servicio eliminado correctamente");
        }
    }
}
