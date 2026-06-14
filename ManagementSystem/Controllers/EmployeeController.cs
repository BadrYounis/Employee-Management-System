using ManagementSystem.Abstractions;
using ManagementSystem.Contracts;
using ManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet("")]
        public async Task<IActionResult>GetEmployees([FromQuery] string? searchValue, CancellationToken cancellationToken)
        {
            return Ok(await _employeeService.GetAllEmployees(searchValue, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.GetEmployeeById(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var result = await _employeeService.CreateEmployee(request, cancellationToken);

            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var result = await _employeeService.UpdateEmployee(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.DeleteEmployee(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> ToggleEmployeeStatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.ToggleEmployeeStatus(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}