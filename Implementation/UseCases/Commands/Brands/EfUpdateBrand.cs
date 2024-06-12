using Application.DTO.Lookup;
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
            EfLookupTable.Update<Brand>(Context, data);
        }
    }
}
