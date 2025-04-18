using Microsoft.AspNetCore.Identity;

namespace EmployeeLeaveManagementAPI.Entities
{
    public  class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
       public string LastName { get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; } = DateTime.UtcNow;
        public bool IsDisabled { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();




    }
}
