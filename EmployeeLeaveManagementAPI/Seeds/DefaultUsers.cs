
namespace EmployeeLeaveManagementAPI.Seeds
{
    public static class DefaultUsers
    {

        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {




            ApplicationUser admin = new ApplicationUser
            {

                UserName = "Admin",

                Email = "admin@Brite.com",
                FirstName = "Admin",
                LastName = "AtBrite",
                EmailConfirmed = true,


            };
            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user is null)
            {

                await userManager.CreateAsync(admin, "P@ssword123");

                await userManager.AddToRoleAsync(admin, ApplicationRoles.Admin);




            }


        }
    }
}
