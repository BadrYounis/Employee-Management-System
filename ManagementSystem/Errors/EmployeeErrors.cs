using ManagementSystem.Abstractions;

namespace ManagementSystem.Errors
{
    public static class EmployeeErrors
    {
        public static readonly Error EmployeeNotFound =
            new("Employee.EmployeeNotFound", "Employee not found", StatusCodes.Status404NotFound);

        public static readonly Error EmailAlreadyExists =
            new("Employee.EmailAlreadyExists", "Email already exists", StatusCodes.Status400BadRequest);

        public static readonly Error DepartmentNotFound =
            new("Department.DepartmentNotFound", "Department not found", StatusCodes.Status404NotFound);
    }
}