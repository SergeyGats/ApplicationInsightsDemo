using ApplicationInsightsDemo.DataAccess.Constants;
using ApplicationInsightsDemo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationInsightsDemo.DataAccess.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable(DatabaseTableNameConstants.Users);
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(DatabaseFieldRestrictionConstants.EmailMaxLength);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(DatabaseFieldRestrictionConstants.FirstNameMaxLength);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(DatabaseFieldRestrictionConstants.LastNameMaxLength);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(DatabaseFieldRestrictionConstants.PasswordHashMaxLength);

            builder.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasMaxLength(DatabaseFieldRestrictionConstants.PasswordSaltMaxLength);

            builder.Property(u => u.CreatedAtUtc)
                .IsRequired()
                .HasColumnType(DatabaseTypeConstants.DefaultDateTime)
                .HasDefaultValueSql(DatabaseFunctionConstants.SysUtcDateTime);

            builder.Property(u => u.UpdatedAtUtc)
                .HasColumnType(DatabaseTypeConstants.DefaultDateTime);

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}