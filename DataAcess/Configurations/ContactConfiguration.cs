using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configurations
{
    internal class ContactConfiguration:EntitiyConfiguration<Contact>
    {
        public override void Configure(EntityTypeBuilder<Contact> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Body).IsRequired().HasMaxLength(400);

            builder.HasIndex(x => x.Email);
        }
    }
}
