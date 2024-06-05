using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configurations
{
    internal class ProductPriceConfiguration:EntitiyConfiguration<Product_Price>
    {
        public override void Configure(EntityTypeBuilder<Product_Price> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Price).IsRequired().HasPrecision(10, 2);
            
            builder.HasOne(x=>x.Product)
                .WithMany(x=>x.Product_Prices)
                .HasForeignKey(x=>x.Product_id)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
