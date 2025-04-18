
using System.Linq.Dynamic.Core;


namespace EmployeeLeaveManagementAPI.Services.Implementations
{
    public class LeaveRequestService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : ILeaveRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<Result<EmployeeLeaveResponse>> CreateleaveAsync(EmployeeLeaveRequest request, CancellationToken cancellationToken = default)
        {

            var user = await GetEmployeeAtBriteById(request.EmployeeId);

            if (user is null)
                return Result.Failure<EmployeeLeaveResponse>(Errors.UserNotFound);


            var IsOverlapping = _unitOfWork.LeaveRequest.IsExists(l => l.EmployeeId == request.EmployeeId && l.Status != LeaveRequestStatus.Rejected
           && (l.StartDate <= request.EndDate && l.EndDate >= request.StartDate));

            if (IsOverlapping)
                return Result.Failure<EmployeeLeaveResponse>(Errors.overlappingLeave);

            var leaveRequest = request.Adapt<LeaveRequest>();

            _unitOfWork.LeaveRequest.Add(leaveRequest);
            _unitOfWork.Complete();
            return Result.Success(leaveRequest.Adapt<EmployeeLeaveResponse>());
        }

        public async Task<Result<PaginatedList<EmployeeLeaveResponse>>> GetAllAsync(string id, RequestFilters filters, CancellationToken cancellationToken = default)
        {
            var user = await GetEmployeeAtBriteById(id);

            if (user is null)
                return Result.Failure<PaginatedList<EmployeeLeaveResponse>>(Errors.UserNotFound);

            var query = _unitOfWork.LeaveRequest.GetQueryable().Where(l => l.EmployeeId == id);


            if (!string.IsNullOrEmpty(filters.SearchValue))
            {

                query = query.Where(q => q.Status.Contains(filters.SearchValue));
            }



            if (!string.IsNullOrEmpty(filters.SortColumn))
            {

                query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }

            var source = query.Include(q => q.Employee).ProjectToType<EmployeeLeaveResponse>();


            var allOfLeavesForhisEmployee = await PaginatedList<EmployeeLeaveResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

            return Result.Success(allOfLeavesForhisEmployee);

        }

        public async Task<Result<EmployeeLeaveResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var leaveRequest = _unitOfWork.LeaveRequest.GetById(id);
            if (leaveRequest is null)
                return Result.Failure<EmployeeLeaveResponse>(Errors.LeaveRequestNotFound);
            var employeeId = leaveRequest.EmployeeId;


            var user = await GetEmployeeAtBriteById(employeeId);

            if (user is null)
                return Result.Failure<EmployeeLeaveResponse>(Errors.UserNotFound);

            var leaveRequestOfThisEmployee = GetLeaveRequestEmployeeAtBriteById(id);


            return Result.Success(leaveRequestOfThisEmployee.Adapt<EmployeeLeaveResponse>());

        }

        public async Task<Result> UpdateApproveRequestAsync(int id, CancellationToken cancellationToken = default)

        {
            var leaveRequestOfThisEmployee = GetLeaveRequestEmployeeAtBriteById(id);


            if (leaveRequestOfThisEmployee is null)
                return Result.Failure(Errors.LeaveRequestNotFound);

            leaveRequestOfThisEmployee.Status = LeaveRequestStatus.Approved;
            _unitOfWork.Complete();
            return Result.Success();

        }

        public async Task<Result> UpdateRejectRequestAsync(int id, CancellationToken cancellationToken = default)
        {

            var leaveRequestOfThisEmployee = GetLeaveRequestEmployeeAtBriteById(id);


            if (leaveRequestOfThisEmployee is null)
                return Result.Failure(Errors.LeaveRequestNotFound);

            leaveRequestOfThisEmployee.Status = LeaveRequestStatus.Rejected;
            _unitOfWork.Complete();
            return Result.Success();

        }

        private LeaveRequest? GetLeaveRequestEmployeeAtBriteById(int id) => _unitOfWork.LeaveRequest.GetById(id);
        private async Task<ApplicationUser?> GetEmployeeAtBriteById(string id) => await _userManager.FindByIdAsync(id);

    }
}
