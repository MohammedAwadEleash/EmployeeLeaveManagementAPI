
namespace EmployeeLeaveManagementAPI.Persistence.EntitiesConfigurations
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.Property(x => x.Reason).HasMaxLength(200);

        }
    }
}
