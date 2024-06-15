using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands.BrandsCommands;
using DataAcess;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Brands
{
    public class EfDeleteBrand : EfUseCase,IDeleteBrandCommand
    {
        public EfDeleteBrand(ASPContext context) : base(context)
        {
        }

        public int Id => 15;

        public string Name => this.GetType().Name;

        public void Execute(DeleteDTO data)
        {
            Brand brand_obj = Context.Brands.Find(data.Id);
            if (brand_obj == null) { throw new EntityNotFoundException(nameof(Brand),data.Id); }
            if(brand_obj.Products.Count > 0) { throw new ConflictException("You cannot delete this brand"); }
            Context.Brands.Remove(brand_obj);
            Context.SaveChanges();
        }
    }
}
