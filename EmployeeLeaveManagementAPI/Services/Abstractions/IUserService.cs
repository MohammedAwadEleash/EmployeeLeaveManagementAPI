


namespace EmployeeLeaveManagementAPI.Services.Abstractions
{
    public interface IUserService
    {
        //Account Setting:
        Task<Result<UserProfileResponse>> GetProfileAsync(string userId);

        Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
        Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);




        // User Management:
        Task<Result<UserResponse>> GetAsync(string id);
        Task<Result<UserResponse>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default);
        Task<Result> ToggleStatusAsync(string id);
        Task<Result> UnlockAsync(string id);

    }
}
