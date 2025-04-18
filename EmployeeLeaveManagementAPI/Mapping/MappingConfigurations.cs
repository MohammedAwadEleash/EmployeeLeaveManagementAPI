
namespace EmployeeLeaveManagementAPI
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


            config.NewConfig<LeaveRequest, EmployeeLeaveResponse>()
                .Map(dest => dest.Name, src => $"{src.Employee.FirstName} {src.Employee.LastName}")
                .Map(dest => dest.Email, src => src.Employee.Email)
                .Map(dest => dest.UserName, src => src.Employee.UserName);


            config.NewConfig<(ApplicationUser user, IList<string> roles), UserResponse>()
               .Map(dest => dest, src => src.user)
               .Map(des => des.Roles, src => src.roles);

        }
    }
}
