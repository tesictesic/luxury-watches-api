using Application.DTO.Lookup;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Color
{
    public class CreateColorDTOValidation : AbstractValidator<ColorDTO>
    {
        private readonly ASPContext _context;
        public CreateColorDTOValidation(ASPContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty()
                              .WithMessage("Color name is required")
                              .MinimumLength(3)
                              .WithMessage("Min number of characters is 3")
                              .Must(name => !_context.Colors.Any(g => g.Name == name))
                              .WithMessage("Color name is in use");
        }
    }
}
