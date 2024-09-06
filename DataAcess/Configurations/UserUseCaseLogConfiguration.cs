using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configurations
{
    internal class UserUseCaseLogConfiguration : IEntityTypeConfiguration<UserUseCaseLog>
    {
        public void Configure(EntityTypeBuilder<UserUseCaseLog> builder)
        {
            builder.Property(x => x.UseCaseName).HasMaxLength(100);
            builder.Property(x=>x.UserName).HasMaxLength(100);
            builder.Property(x=>x.UserCaseData).HasMaxLength(1000);

            builder.HasIndex(x => new { x.UserName, x.UseCaseName }).IncludeProperties(x => x.UserCaseData);
        }
    }
}
