using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Models.DTOs;
using PODBookingSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PODBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePackageController : ControllerBase
    {
        private readonly IServicePackageService _servicePackageService;

        public ServicePackageController(IServicePackageService servicePackageService)
        {
            _servicePackageService = servicePackageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicePackageDTO>>> GetAllServicePackages()
        {
            var servicePackages = await _servicePackageService.GetAllServicePackages();
            return Ok(servicePackages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePackageDTO>> GetServicePackageById(int id)
        {
            var servicePackage = await _servicePackageService.GetServicePackageById(id);
            if (servicePackage == null) return NotFound();
            return Ok(servicePackage);
        }

        [HttpPost]
        public async Task<ActionResult<ServicePackageDTO>> CreateServicePackage(ServicePackageDTO servicePackageDto)
        {
            var createdServicePackage = await _servicePackageService.CreateServicePackage(servicePackageDto);
            return CreatedAtAction(nameof(GetServicePackageById), new { id = createdServicePackage.ServicePackageId }, createdServicePackage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateServicePackage(int id, ServicePackageDTO servicePackageDto)
        {
            if (id != servicePackageDto.ServicePackageId) return BadRequest();

            var result = await _servicePackageService.UpdateServicePackage(servicePackageDto);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServicePackage(int id)
        {
            var result = await _servicePackageService.DeleteServicePackage(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
