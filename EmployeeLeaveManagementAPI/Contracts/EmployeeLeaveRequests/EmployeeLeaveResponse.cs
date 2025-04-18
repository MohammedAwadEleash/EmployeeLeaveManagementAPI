
namespace EmployeeLeaveManagementAPI.Contracts
{

    public record EmployeeLeaveResponse(string Id, string Name, string UserName, string Email, DateTime StartDate, DateTime EndDate, string Reason, string Status);

}
