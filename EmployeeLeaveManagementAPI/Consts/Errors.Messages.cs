
namespace EmployeeLeaveManagementAPI.Consts;

public static class Errors
{
    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status400BadRequest);

    public static readonly Error DisabledUser =
      new("User.DisabledUser", "Disabled User, Please contact your Administrator", StatusCodes.Status401Unauthorized);



    public static readonly Error LockedUser =
      new("User.LockedUser", "Locked User, Please contact your Administrator", StatusCodes.Status401Unauthorized);


    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status400BadRequest);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status400BadRequest);


    public static readonly Error DuplicatedEmail =
       new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);




    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);


    public static readonly Error UserNotFound =
        new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);
    public static readonly Error RequestNotFound =
        new("User.RequestNotFound", "Request of Leave is not found", StatusCodes.Status404NotFound);
    public static readonly Error LeaveRequestNotFound =
    new("LeaveRequestNotFound.NotFound", "Leave Request is not found", StatusCodes.Status404NotFound);

    public static readonly Error overlappingLeave =
    new("User.overlappingLeave ", "You already have a leave request that overlaps with these dates", StatusCodes.Status409Conflict);

}

