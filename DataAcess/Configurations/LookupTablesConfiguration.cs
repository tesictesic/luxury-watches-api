﻿using Domain.LookupTables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configurations
{
    internal class GenderConfiguration:LookupNameConfiguration<Gender>
    {
        public override void Configure(EntityTypeBuilder<Gender> builder)
        {
            base.Configure(builder);
        }
    }
    internal class BrandConfiguration : LookupNameConfiguration<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
    internal class RoleConfiguration : LookupNameConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
        }
    }
    internal class SpecificationConfiguration : LookupNameConfiguration<Specification>
    {
        public override void Configure(EntityTypeBuilder<Specification> builder)
        {
            base.Configure(builder);
        }
    }
    internal class ColorConfiguration : LookupNameConfiguration<Color>
    {
        public override void Configure(EntityTypeBuilder<Color> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Class).IsRequired().HasMaxLength(100);
        }
    }
}