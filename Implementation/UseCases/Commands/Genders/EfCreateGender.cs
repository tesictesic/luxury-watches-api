using Application.DTO.Lookup;
using Application.UseCases.Commands.GenderCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.UseCases.Commands;
using Implementation.Validations.Gender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Genders
{
    public class EfCreateGender : EfUseCase, ICreateGenderCommand
    {
        private CreateGenderDTOValidation _validator;
        public EfCreateGender(CreateGenderDTOValidation validations, ASPContext context) : base(context)
        {
            _validator = validations;
        }

        public int Id => 2;

        public string Name => GetType().Name;

        public void Execute(GenderDTO data)
        {
            _validator.ValidateAndThrow(data);


            EfLookupTable.Add<Gender,GenderDTO>(Context, data);


        }
    }
}
