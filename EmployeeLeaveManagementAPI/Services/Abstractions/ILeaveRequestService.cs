

namespace EmployeeLeaveManagementAPI.Services.Abstractions
{
    public interface ILeaveRequestService
    {
         Task<Result<PaginatedList<EmployeeLeaveResponse>>> GetAllAsync(string id, RequestFilters filters, CancellationToken cancellationToken = default);

         Task<Result<EmployeeLeaveResponse>> GetByIdAsync( int  id,CancellationToken cancellationToken = default);

         Task<Result<EmployeeLeaveResponse>> CreateleaveAsync(EmployeeLeaveRequest request, CancellationToken cancellationToken = default);
         Task<Result> UpdateApproveRequestAsync(int  id,CancellationToken cancellationToken = default);
         Task<Result> UpdateRejectRequestAsync(int  id,CancellationToken cancellationToken = default);

    }
}
