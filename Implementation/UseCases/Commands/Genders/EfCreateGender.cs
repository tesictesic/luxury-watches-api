using Application.DTO;
using Application.UseCases.Commands.GenderCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.UseCases.Commands;
using Implementation.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Genders
{
    public class EfCreateGender : EfUseCase, ICreateGenderCommand
    {
        private CreateUpdateGenderDTOValidation _validator;
        public EfCreateGender(CreateUpdateGenderDTOValidation validations, ASPContext context) : base(context)
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
