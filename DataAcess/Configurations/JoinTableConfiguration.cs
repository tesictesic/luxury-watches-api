using Domain.Join_Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configurations
{
   internal class ProductColorConfiguration : EntitiyConfiguration<Product_Color>
    {
        public override void Configure(EntityTypeBuilder<Product_Color> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductColors)
                .HasForeignKey(x => x.Product_id)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasOne(x=>x.Color)
                   .WithMany(x=>x.ProductColors)
                   .HasForeignKey(x=>x.Color_id)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
    internal class ProductSpecificationConfiguration : EntitiyConfiguration<Product_Specification>
    {
        public override void Configure(EntityTypeBuilder<Product_Specification> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Value).IsRequired().HasMaxLength(100);

            builder.HasOne(x=>x.Product)
                    .WithMany(x=>x.Product_Specifications)
                    .HasForeignKey(x=>x.Product_id)
                    .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasOne(x=>x.Specification)
                   .WithMany(x=>x.Product_Specifications)
                   .HasForeignKey(x=>x.Specification_id)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        }
    }
    internal class ProductCartConfiuration : EntitiyConfiguration<Product_Cart>
    {
        public override void Configure(EntityTypeBuilder<Product_Cart> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.Product_Carts)
                   .HasForeignKey(x => x.Product_id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Cart)
                   .WithMany(x => x.Product_Carts)
                   .HasForeignKey(x => x.Cart_id)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
