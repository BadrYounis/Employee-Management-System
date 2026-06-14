using FluentValidation;
using ManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Contracts
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        private readonly ApplicationDbContext _context;
        public CreateEmployeeRequestValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .MustAsync(BeUniqueEmail)
                .WithMessage("Email is already exists");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^\d{10,15}$")
                .WithMessage("Invalid phone number");

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .WithMessage("Job title is required")
                .Length(2, 50);

            RuleFor(x => x.HireDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Hire date cannot be in the future");

            RuleFor(x => x.DepartmentId)
                .MustAsync(DepartmentExists)
                .WithMessage("Department does not exist");
        }
        private async Task<bool> DepartmentExists(int departmentId, CancellationToken cancellationToken)
        {
            return await _context.Departments.AnyAsync(d => d.Id == departmentId, cancellationToken);
        }
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _context.Employees.AnyAsync(e => e.Email == email, cancellationToken);
        }
    }
}