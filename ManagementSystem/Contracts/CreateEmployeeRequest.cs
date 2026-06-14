namespace ManagementSystem.Contracts
{
    public record CreateEmployeeRequest(
        string FullName,
        string Email,
        string PhoneNumber,
        string JobTitle,
        DateOnly? HireDate,
        int DepartmentId
    );
}