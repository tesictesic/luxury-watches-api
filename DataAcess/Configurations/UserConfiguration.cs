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
    internal class UserConfiguration:EntitiyConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.First_Name).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Last_Name).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Password).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Picture).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.isBanned).HasDefaultValue(false);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Password);

            builder.HasOne(x => x.Role)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.Role_id)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
