using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.DTOs.NailService;
using NailSalon.Application.Interfaces.Services;

namespace NailSalon.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly INailServiceLogic _nailServiceLogic;

    public ServicesController(INailServiceLogic nailServiceLogic)
    {
        _nailServiceLogic = nailServiceLogic;
    }

    // GET: api/services
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _nailServiceLogic.GetAllServicesAsync();
        return Ok(services);
    }

    // GET: api/services/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var service = await _nailServiceLogic.GetServiceByIdAsync(id);
        if (service == null) return NotFound("Không tìm thấy dịch vụ.");

        return Ok(service);
    }

    // POST: api/services
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpdateNailServiceDto dto)
    {
        try
        {
            var newService = await _nailServiceLogic.CreateServiceAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newService.Id }, newService);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/services/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateNailServiceDto dto)
    {
        try
        {
            await _nailServiceLogic.UpdateServiceAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: api/services/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _nailServiceLogic.DeleteServiceAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}