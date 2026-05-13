using Microsoft.AspNetCore.Mvc;
using NailSalon.Application.DTOs.Customer;
using NailSalon.Application.Interfaces;

namespace NailSalon.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: api/customers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    // GET: api/customers/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound("Không tìm thấy khách hàng.");

        return Ok(customer);
    }

    // POST: api/customers
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpdateCustomerDto dto)
    {
        try
        {
            var newCustomer = await _customerService.CreateCustomerAsync(dto);
            // Trả về HTTP 201 Created kèm theo URL để lấy thông tin khách hàng vừa tạo
            return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, newCustomer);
        }
        catch (Exception ex)
        {
            // Bắt lỗi trùng số điện thoại đã ném ra từ Service
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/customers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateCustomerDto dto)
    {
        try
        {
            await _customerService.UpdateCustomerAsync(id, dto);

            // Chuyển sang trả về Ok kèm object chứa message
            return Ok(new
            {
                message = "Cập nhật thông tin khách hàng thành công!",
                updatedAt = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: api/customers/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent(); // Bị chặn lại bởi Interceptor và tự động đổi thành Soft Delete
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}