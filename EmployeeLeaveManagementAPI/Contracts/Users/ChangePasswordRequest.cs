namespace EmployeeLeaveManagementAPI.Contracts.Users
{
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword);

}
