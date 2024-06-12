using Application.DTO.Lookup;
using Application.UseCases.Commands.SpecificationCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.Validations.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Specifications
{
    public class EfUpdateSpecification : EfUseCase,IUpdateSpecificationCommand
    {
        private UpdateSpecificationDTOValidation _validation;
        public EfUpdateSpecification(UpdateSpecificationDTOValidation valid, ASPContext context):base(context)
        {
            this._validation = valid;
        }
        public int Id => 13;

        public string Name => this.GetType().Name;

        public void Execute(SpecificationDTO data)
        {
            this._validation.ValidateAndThrow(data);
            EfLookupTable.Update<Specification>(Context, data);
        }
    }
}
