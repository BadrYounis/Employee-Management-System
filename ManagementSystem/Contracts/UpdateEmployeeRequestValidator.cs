using FluentValidation;
using ManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Contracts
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        private readonly ApplicationDbContext _context;
        public UpdateEmployeeRequestValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^\d{10,15}$")
                .WithMessage("Invalid phone number");

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .WithMessage("Job title is required")
                .MaximumLength(50);

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage("Invalid department ID")
                .MustAsync(DepartmentExists)
                .WithMessage("Department does not exist");
        }
        private async Task<bool> DepartmentExists(int departmentId, CancellationToken cancellationToken)
        {
            return await _context.Departments.AnyAsync(d => d.Id == departmentId, cancellationToken);
        }
    }
}