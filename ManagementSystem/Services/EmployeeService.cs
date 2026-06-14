using ManagementSystem.Abstractions;
using ManagementSystem.Contracts;
using ManagementSystem.Entities;
using ManagementSystem.Errors;
using ManagementSystem.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Services
{
    public class EmployeeService(ApplicationDbContext context) : IEmployeeService
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployees(string? searchValue, CancellationToken cancellationToken = default) =>
            await _context.Employees
                .AsNoTracking()
                .Where(e => (string.IsNullOrWhiteSpace(searchValue)
                || e.FullName.Contains(searchValue)
                || e.Department.Name.Contains(searchValue)))
                .ProjectToType<EmployeeResponse>()
                .ToListAsync(cancellationToken);
        public async Task<Result<EmployeeResponse>> GetEmployeeById(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == id)
                .ProjectToType<EmployeeResponse>()
                .FirstOrDefaultAsync(cancellationToken);

            return employee is not null
                ? Result.Success(employee)
                : Result.Failure<EmployeeResponse>(EmployeeErrors.EmployeeNotFound);
        }
        public async Task<Result<EmployeeResponse>> CreateEmployee(CreateEmployeeRequest employeeRequest, CancellationToken cancellationToken = default)
        {
            var employee = employeeRequest.Adapt<Employee>();

            employee.IsActive = true;

            await _context.AddAsync(employee, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = await _context.Employees
                .Where(e => e.Id == employee.Id)
                .ProjectToType<EmployeeResponse>()
                .FirstAsync(cancellationToken);

            return Result.Success(response);
        }
        public async Task<Result> UpdateEmployee(int id, UpdateEmployeeRequest employeeRequest, CancellationToken cancellationToken = default)
        {
            var currentEmployee = await _context.Employees.FindAsync(id, cancellationToken);

            if (currentEmployee is null)
                return Result.Failure(EmployeeErrors.EmployeeNotFound);

            var departmentExists = await _context.Departments
                .AnyAsync(d => d.Id == employeeRequest.DepartmentId,cancellationToken);

            if (!departmentExists)
                return Result.Failure(EmployeeErrors.DepartmentNotFound);

            employeeRequest.Adapt(currentEmployee);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteEmployee(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.FindAsync(id, cancellationToken);

            if (employee is null)
                return Result.Failure(EmployeeErrors.EmployeeNotFound);

            _context.Remove(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> ToggleEmployeeStatus(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.FindAsync(id, cancellationToken);

            if (employee is null)
                return Result.Failure(EmployeeErrors.EmployeeNotFound);

            employee.IsActive = !employee.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}