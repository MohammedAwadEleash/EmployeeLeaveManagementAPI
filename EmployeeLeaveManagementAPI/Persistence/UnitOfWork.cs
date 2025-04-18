using EmployeeLeaveManagementAPI.Repositories.Abstractions;
using EmployeeLeaveManagementAPI.Repositories.Implementations;

namespace EmployeeLeaveManagementAPI.Persistence
{
    public class UnitOfWork(ApplicationDbContext _context): IUnitOfWork
    {
        private readonly ApplicationDbContext _context = _context;

        public IBaseRepository<LeaveRequest> LeaveRequest => new BaseRepository<LeaveRequest>(_context);
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}

