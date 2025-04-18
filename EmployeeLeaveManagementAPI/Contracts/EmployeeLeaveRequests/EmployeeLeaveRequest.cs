namespace EmployeeLeaveManagementAPI.Contracts.EmployeeLeaveRequests
{
    public record   EmployeeLeaveRequest(string EmployeeId, DateTime StartDate,DateTime EndDate, string Reason);
    
}
