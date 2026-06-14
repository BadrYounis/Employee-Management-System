using ManagementSystem.Abstractions;
using ManagementSystem.Contracts;

namespace ManagementSystem.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeResponse>> GetAllEmployees(string? searchValue, CancellationToken cancellationToken = default);
        public Task<Result<EmployeeResponse>> GetEmployeeById(int id, CancellationToken cancellationToken = default);
        public Task<Result<EmployeeResponse>> CreateEmployee(CreateEmployeeRequest employeeRequest, CancellationToken cancellationToken = default);
        public Task<Result> UpdateEmployee(int id, UpdateEmployeeRequest employeeRequest, CancellationToken cancellationToken = default);
        public Task<Result> DeleteEmployee(int id, CancellationToken cancellationToken = default);
        public Task<Result> ToggleEmployeeStatus(int id, CancellationToken cancellationToken = default);
    }
}