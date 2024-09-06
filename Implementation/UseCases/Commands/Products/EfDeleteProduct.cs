using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands.ProductCommands;
using DataAcess;
using Domain;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Products
{
    public class EfDeleteProduct : EfUseCase,IDeleteProductCommand
    {
        public EfDeleteProduct(ASPContext context) : base(context)
        {
        }

        public int Id => 19;

        public string Name => this.GetType().Name;

        public void Execute(DeleteDTO data)
        {
            Product product_obj = Context.Products.Find(data.Id);
            if(product_obj== null) { throw new EntityNotFoundException(nameof(Product), data.Id); }
            if (product_obj.Product_Carts.Count > 0) { throw new ConflictException("You cannot delete this product"); }
            Product_Price product_price = product_obj.Product_Prices.FirstOrDefault(y => y.Product_id == data.Id);
            if (product_price == null)
            {
                throw new EntityNotFoundException(nameof(Product_Price), data.Id);
            }
            Context.Product_Prices.Remove(product_price);
            var product_colors=product_obj.ProductColors.Where(y=>y.Product_id==data.Id);
            if(product_colors!=null || product_colors.Any())
            {
                Context.Product_Colors.RemoveRange(product_colors);
                Context.SaveChanges();
            }

            Context.SaveChanges();
            Context.Products.Remove(product_obj);
            Context.SaveChanges();
        }
    }
}
