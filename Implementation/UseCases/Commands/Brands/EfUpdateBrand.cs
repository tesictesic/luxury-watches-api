using Application.DTO;
using Application.UseCases.Commands.BrandsCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Brands
{
    public class EfUpdateBrand : EfUseCase,IUpdateBrandCommand
    {
        private readonly CreateUpdateBrandsDTOValdiation validations;
        public EfUpdateBrand(CreateUpdateBrandsDTOValdiation validations,ASPContext context):base(context) 
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
