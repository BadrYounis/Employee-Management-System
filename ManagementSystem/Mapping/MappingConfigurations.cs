using ManagementSystem.Contracts;
using ManagementSystem.Entities;
using Mapster;

namespace ManagementSystem.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Employee, EmployeeResponse>()
                .Map(dest => dest.DepartmentName, src => src.Department.Name);

            config.NewConfig<CreateEmployeeRequest, Employee>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Department);

            config.NewConfig<UpdateEmployeeRequest, Employee>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Email)
                .Ignore(dest => dest.FullName)
                .Ignore(dest => dest.HireDate)
                .Ignore(dest => dest.IsActive)
                .Ignore(dest => dest.Department);
        }
    }
}