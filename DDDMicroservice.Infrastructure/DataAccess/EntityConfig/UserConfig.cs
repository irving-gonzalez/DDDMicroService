using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDMicroservice.Domain.AggregatesModel;

namespace DDDMicroservice.Infrastructure.DataAccess.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            //[Required]
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();

            // //Type
            // builder.Property(p => p.ApplicantType).HasColumnName("ApplicantTypeId").IsRequired();
            // builder.HasOne<ApplicantType>().WithMany().HasForeignKey(b => b.ApplicantType);
        }
    }
}