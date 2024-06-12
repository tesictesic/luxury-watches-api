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
    public class EfUpdateGender : EfUseCase, IUpdateGenderCommand
    {
        private UpdateGenderDTOValidation _validator;
        public EfUpdateGender(UpdateGenderDTOValidation validatons, ASPContext context) : base(context)
        {
            _validator = validatons;
        }

        public int Id => 3;

        public string Name => GetType().Name;

        public void Execute(GenderDTO data)
        {
            _validator.ValidateAndThrow(data);

            EfLookupTable.Update<Gender>(Context, data);



        }
    }
}
