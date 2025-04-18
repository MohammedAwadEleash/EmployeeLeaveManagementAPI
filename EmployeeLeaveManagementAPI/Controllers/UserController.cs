﻿

namespace EmployeeLeaveManagementAPI.Controllers
{
    [Route("Brite/[controller]s")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]

    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;


        [HttpGet("{id}")]

        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _userService.GetAsync(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }


        [HttpPost("")]

        public async Task<IActionResult> Add([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddAsync(request, cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();


        }


        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateAsync(id, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();


        }

        [HttpPut("{id}/togglestatus")]
        public async Task<IActionResult> ToggleStatus([FromRoute] string id)
        {
            var result = await _userService.ToggleStatusAsync(id);

            return result.IsSuccess ? NoContent() : result.ToProblem();

        }

        [HttpPut("{id}/unlock")]
        public async Task<IActionResult> Unlock([FromRoute] string id)
        {
            var result = await _userService.UnlockAsync(id);

            return result.IsSuccess ? NoContent() : result.ToProblem();

        }
    }
}
