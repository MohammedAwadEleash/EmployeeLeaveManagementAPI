using EmployeeLeaveManagementAPI.Consts;
using EmployeeLeaveManagementAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeLeaveManagementAPI.Persistence
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
                    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

    

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }

        public DbSet <LeaveRequest> leaveRequests { get; set; }


    }
}
