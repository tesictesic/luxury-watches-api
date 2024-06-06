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
    public class EfCreateBrand : EfUseCase,ICreateBrandCommand
    {

        private CreateUpdateBrandsDTOValdiation _validation;
        public EfCreateBrand(CreateUpdateBrandsDTOValdiation validations, ASPContext context) : base(context)
        {
            this._validation = validations;
        }

        public int Id => 4;

        public string Name => this.GetType().Name;

        public void Execute(BrandDTO data)
        {
            _validation.ValidateAndThrow(data);
            EfLookupTable.Add<Brand,BrandDTO>(Context, data);
        }
    }
}
