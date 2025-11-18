using HireAI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireAI.Data.Configurations
{
    public class HRConfiguration : IEntityTypeConfiguration<HR>
    {
        public void Configure(EntityTypeBuilder<HR> builder)
        {
            builder.Property(hr => hr.CompanyName)
                .HasMaxLength(200);

            builder.Property(hr => hr.AccountType)
                .HasDefaultValue(0); // AccountType.Free

            builder.Property(hr => hr.PremiumExpiry)
                .IsRequired(false);

   

  

            builder.HasMany(hr => hr.Payments)
                .WithOne(p => p.HR)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(hr => hr.CompanyName);
            builder.HasIndex(hr => hr.AccountType);
        }
    }
}
