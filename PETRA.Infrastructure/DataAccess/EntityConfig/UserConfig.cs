using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PETRA.Domain.AggregatesModel;

namespace PETRA.Infrastructure.DataAccess.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Applicant");

            //[Required]
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();

            // //Type
            // builder.Property(p => p.ApplicantType).HasColumnName("ApplicantTypeId").IsRequired();
            // builder.HasOne<ApplicantType>().WithMany().HasForeignKey(b => b.ApplicantType);
        }
    }
}