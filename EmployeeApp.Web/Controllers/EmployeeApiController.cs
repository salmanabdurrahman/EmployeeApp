using Microsoft.AspNetCore.Mvc;
using EmployeeApp.Web.Services;
using EmployeeApp.Web.Models;

namespace EmployeeApp.Web.Controllers;

/// <summary>
/// API Controller for Kendo UI Grid data operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmployeeApiController : ControllerBase
{
    private readonly EmployeeApiService _apiService;
    private readonly ILogger<EmployeeApiController> _logger;

    public EmployeeApiController(EmployeeApiService apiService, ILogger<EmployeeApiController> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }

    // GET: api/EmployeeApi
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        try
        {
            var employees = await _apiService.GetAllEmployeesAsync();
            return Ok(employees ?? new List<Employee>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employees");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // GET: api/EmployeeApi/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        try
        {
            var employee = await _apiService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();
            
            return Ok(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employee {Id}", id);
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // POST: api/EmployeeApi
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
    {
        try
        {
            var (success, message, errors) = await _apiService.CreateEmployeeAsync(employee);
            
            if (success)
                return Ok(new { message });
            
            return BadRequest(new { message, errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // PUT: api/EmployeeApi/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
    {
        try
        {
            if (id != employee.Id)
                return BadRequest(new { message = "ID mismatch" });

            var (success, message, errors) = await _apiService.UpdateEmployeeAsync(id, employee);
            
            if (success)
                return Ok(new { message });
            
            return BadRequest(new { message, errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee {Id}", id);
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // DELETE: api/EmployeeApi/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            var (success, message) = await _apiService.DeleteEmployeeAsync(id);
            
            if (success)
                return Ok(new { message });
            
            return BadRequest(new { message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee {Id}", id);
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
