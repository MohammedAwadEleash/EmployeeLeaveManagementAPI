



namespace EmployeeLeaveManagementAPI.Services.Implementations
{
    public class UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<UserResponse>> GetAsync(string id)

        {

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return Result.Failure<UserResponse>(Errors.UserNotFound);

            var userRoles = await _userManager.GetRolesAsync(user);

            var response = (user, userRoles).Adapt<UserResponse>();

            return Result.Success(response);

        }





        public async Task<Result<UserResponse>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {


            var emailIsExists = await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);
            if (emailIsExists)
                return Result.Failure<UserResponse>(Errors.DuplicatedEmail);

            var user = request.Adapt<ApplicationUser>();


            var result = await _userManager.CreateAsync(user, request.Password);
            user.EmailConfirmed = true;

            foreach (var roleName in request.Roles)
            {

                var roleIsExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleIsExists)
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, request.Roles);
                var response = (user, request.Roles).Adapt<UserResponse>();
                return Result.Success(response);
            }



            var error = result.Errors.First();

            return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }



        public async Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default)
        {



            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return Result.Failure(Errors.UserNotFound);


            var emailIsExists = await _userManager.Users.AnyAsync(u => u.Email == request.Email && u.Id != id, cancellationToken);
            if (emailIsExists)
                return Result.Failure(Errors.DuplicatedEmail);

            await _context.UserRoles.Where(r => r.UserId == id)
                  .ExecuteDeleteAsync(cancellationToken);

            foreach (var roleName in request.Roles)
            {

                var roleIsExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleIsExists)
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            user = request.Adapt(user);


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {



                await _context.UserRoles.Where(r => r.UserId == id)
                    .ExecuteDeleteAsync(cancellationToken);


                await _userManager.AddToRolesAsync(user, request.Roles);
                return Result.Success();

            }

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }



        public async Task<Result> ToggleStatusAsync(string id)

        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return Result.Failure(Errors.UserNotFound);

            user.IsDisabled = !user.IsDisabled;

            await _userManager.UpdateAsync(user);

            return Result.Success();

        }


        public async Task<Result> UnlockAsync(string id)

        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return Result.Failure(Errors.UserNotFound);


            await _userManager.SetLockoutEndDateAsync(user, null);

            return Result.Success();


        }

        //Account Anformation:
        public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId)
                .ProjectToType<UserProfileResponse>().SingleAsync();


            return Result.Success(user);
        }



        public async Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user = request.Adapt(user);

            await _userManager.UpdateAsync(user!);
            return Result.Success();

        }


        public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);


            if (result.Succeeded)
                return Result.Success();


            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


        }

    }

}
