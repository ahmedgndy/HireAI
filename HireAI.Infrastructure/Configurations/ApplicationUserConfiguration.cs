using HireAI.Data.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration:IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(u => u.Applicant).WithOne().OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(u => u.HR).WithOne().OnDelete(DeleteBehavior.SetNull);

        }
    }
}
