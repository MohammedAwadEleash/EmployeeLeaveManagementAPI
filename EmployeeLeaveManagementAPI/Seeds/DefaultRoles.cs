
namespace EmployeeLeaveManagementAPI.Seeds
{
    public static class DefaultRoles
    {

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManger)
        {

            if (!roleManger.Roles.Any())
            {

                await roleManger.CreateAsync(new IdentityRole(ApplicationRoles.Admin));
               

            }
        }



    }
}
