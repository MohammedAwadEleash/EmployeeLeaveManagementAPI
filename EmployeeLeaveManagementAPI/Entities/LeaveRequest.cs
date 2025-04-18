
namespace EmployeeLeaveManagementAPI.Entities
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Reason { get; set; } = String.Empty;
        public string Status { get; set; } = LeaveRequestStatus.Pending;// Pending, Approved, Rejected


        public ApplicationUser Employee { get; set; } = default!;


    }
}
