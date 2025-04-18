

using System.Security.Cryptography;



namespace EmployeeLeaveManagementAPI.Services.Implementations
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtProvider jwtProvider, ILogger<AuthService> logger,
        IEmailSender emailSender,
     ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


        private readonly int _refreshTokenExpiryDay = 14;
        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)

                return Result.Failure<AuthResponse>(Errors.InvalidCredentials);

            if (user.IsDisabled)
                return Result.Failure<AuthResponse>(Errors.DisabledUser);




            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

            if (result.Succeeded)
            {


                var userRoles = await GetUserRolesAndPermissions(user, cancellationToken);




                // Generate Jwt Token:
                var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);




                // Generate RefreshToken:

                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDay);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpration

                });
                await _userManager.UpdateAsync(user);

                var authResponse = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpration);

                return Result.Success(authResponse);
            }

            var error = result.IsNotAllowed ? Errors.EmailNotConfirmed
                : result.IsLockedOut ? Errors.LockedUser
                : Errors.InvalidCredentials;

            return Result.Failure<AuthResponse>(error);


        }



        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {

            // 1-  Validate (JWT Token) :
            var userId = _jwtProvider.ValidateToken(token);
            // this userId came with Request (JWT Token)
            // validate this  Token 
            if (userId is null)
                return Result.Failure<AuthResponse>(Errors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure<AuthResponse>(Errors.InvalidJwtToken);


            if (user.IsDisabled)
                return Result.Failure<AuthResponse>(Errors.DisabledUser);


            if (user.LockoutEnd > DateTime.UtcNow)
                return Result.Failure<AuthResponse>(Errors.LockedUser);


            // 2-  Validate (RefreshToken) :

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(r => r.Token == refreshToken && r.IsActive);



            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(Errors.InvalidRefreshToken);

            // make the current Refresh Token InActive /RevokedOn:
            userRefreshToken.RevokedOn = DateTime.UtcNow;

            // Generate new (Jwt Token):

            var userRoles = await GetUserRolesAndPermissions(user, cancellationToken);

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);


            // Generte RefreshToken:

            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDay);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpration

            });
            await _userManager.UpdateAsync(user);


            var authResponse = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn, newRefreshToken, refreshTokenExpration);
            return Result.Success(authResponse);

        }



        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {

            // 1-  Validate (JWT Token) :
            var userId = _jwtProvider.ValidateToken(token);
            // this userId came with Request 
            // validate this  Token 

            if (userId is null)
                return Result.Failure(Errors.InvalidJwtToken);


            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure(Errors.InvalidJwtToken);



            // 2-  Validate (RefreshToken) :

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(r => r.Token == refreshToken && r.IsActive);

            if (userRefreshToken is null)
                return Result.Failure(Errors.InvalidRefreshToken);

            // make the cuurent Refresh Token InActive /RevokedOn:
            userRefreshToken.RevokedOn = DateTime.UtcNow;


            await _userManager.UpdateAsync(user);

            return Result.Success();

        }



        private static string GenerateRefreshToken()
        {

            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        }

        private async Task<IEnumerable<string>> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
            => await _userManager.GetRolesAsync(user);




    }
}
