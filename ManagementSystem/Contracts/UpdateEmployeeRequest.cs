namespace ManagementSystem.Contracts
{
    public record UpdateEmployeeRequest(
        string PhoneNumber,
        string JobTitle,
        int DepartmentId
    );
}