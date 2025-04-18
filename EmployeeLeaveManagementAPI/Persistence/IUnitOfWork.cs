
namespace EmployeeLeaveManagementAPI.Persistence
{
    public interface IUnitOfWork
    {

        IBaseRepository<LeaveRequest> LeaveRequest { get; }
        public int Complete();
    }
}
