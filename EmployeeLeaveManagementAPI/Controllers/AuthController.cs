﻿using Microsoft.AspNetCore.Identity.Data;

namespace EmployeeLeaveManagementAPI.Controllers
{
    [Route("Brite/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;


        [HttpPost("")]

        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Logging with email: {email} and password: {password}", request.Email, request.Password);



            var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);



            return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();

        }
        [HttpPost("refresh")]

        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {

            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);


            return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();

        }

        [HttpPost("revoke-refresh-token")]

        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {

            var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem();


        }



    }
}

