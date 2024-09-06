using Application.DTO.Lookup;
using Application.Exceptions;
using Application.UseCases.Commands.BrandsCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.Validations.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Brands
{
    public class EfUpdateBrand : EfUseCase,IUpdateBrandCommand
    {
        private readonly UpdateBrandsDTOValidation validations;
        public EfUpdateBrand(UpdateBrandsDTOValidation validations,ASPContext context):base(context) 
        {
            this.validations = validations;
        }
        public int Id => 5;

        public string Name => this.GetType().Name;

        public void Execute(BrandDTO data)
        {
            validations.ValidateAndThrow(data);
            var brand_obj=this.Context.Brands.Find(data.Id);
            if (brand_obj == null) throw new EntityNotFoundException("That entity doesn't exist",data.Id);
            brand_obj.Name = data.Name;
            brand_obj.Description = data.Description;
            Context.SaveChanges();
        }
    }
}
