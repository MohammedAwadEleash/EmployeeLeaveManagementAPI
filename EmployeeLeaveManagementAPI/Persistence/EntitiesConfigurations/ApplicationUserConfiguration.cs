
namespace EmployeeLeaveManagementAPI.Persistence.EntitiesConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(user => user.FirstName).HasMaxLength(100);
            builder.Property(user => user.LastName).HasMaxLength(100);
            builder.OwnsMany(x => x.RefreshTokens).ToTable("RefreshTokens")
                .WithOwner().HasForeignKey("UserId");

        }
    }
}
