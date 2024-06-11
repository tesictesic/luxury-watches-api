using Application.DTO;
using Application.UseCases.Commands.SpecificationCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Specifications
{
    public class EfCreateSpecification : EfUseCase,ICreateSpecification
    {
        private readonly CreateUpdateSpecificationDTOValidation validations;
        public EfCreateSpecification(CreateUpdateSpecificationDTOValidation validations,ASPContext context):base(context) 
        {
            this.validations = validations;
        }
        public int Id => 9;

        public string Name => this.GetType().Name;

        public void Execute(SpecificationDTO data)
        {
            this.validations.ValidateAndThrow(data);
            Specification specification = new Specification
            {
                Name = data.Name
            };
            Context.Specifications.Add(specification);
            Context.SaveChanges();
            
        }
    }
}
