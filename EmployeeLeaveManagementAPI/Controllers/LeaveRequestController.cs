
namespace EmployeeLeaveManagementAPI.Controllers
{
    [Route("Brite/[controller]s")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class LeaveRequestController(ILeaveRequestService leaveRequestService) : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService = leaveRequestService;


        [HttpGet("{employeeId}/GetAll")]
        public async Task<IActionResult> GetAll([FromRoute] string employeeId, [FromQuery] RequestFilters request, CancellationToken cancellationToken)
        {
            var result = await _leaveRequestService.GetAllAsync(employeeId, request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
        {

            var result = await _leaveRequestService.GetByIdAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] EmployeeLeaveRequest request, CancellationToken cancellationToken)
        {
            var result = await _leaveRequestService.CreateleaveAsync(request, cancellationToken);

            return result.IsSuccess ?
           CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
           : result.ToProblem();
        }

        [HttpPut("{id}/approve-request")]
        public async Task<IActionResult> ApproveRequest([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _leaveRequestService.UpdateApproveRequestAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();

        }
        [HttpPut("{id}/reject-request")]
        public async Task<IActionResult> RejectRequest([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _leaveRequestService.UpdateRejectRequestAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();

        }




    }
}
