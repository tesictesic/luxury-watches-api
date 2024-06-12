using Application.DTO.Lookup;
using Application.UseCases.Commands.ColorCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.Validations.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Colors
{
    public class EfUpdateColor : EfUseCase,IUpdateColorCommand
    {
        private UpdateColorDTOValidation validation;
        public EfUpdateColor(UpdateColorDTOValidation valid,ASPContext context) : base(context)
        {
            this.validation=valid;
        }

        public int Id => 14;

        public string Name => this.GetType().Name;

        public void Execute(ColorDTO data)
        {
            this.validation.ValidateAndThrow(data);

            EfLookupTable.Update<Color>(Context, data);
        }
    }
}
