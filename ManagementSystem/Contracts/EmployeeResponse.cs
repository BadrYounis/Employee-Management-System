namespace ManagementSystem.Contracts
{
    public record EmployeeResponse(
        int Id,
        string FullName,
        string Email,
        string PhoneNumber,
        string JobTitle,
        DateOnly HireDate,
        bool IsActive,
        int DepartmentId,
        string DepartmentName
    );   
}