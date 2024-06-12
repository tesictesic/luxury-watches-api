using Application.DTO.Lookup;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Gender
{
    public class UpdateGenderDTOValidation : AbstractValidator<GenderDTO>
    {
        private readonly ASPContext _context;
        public UpdateGenderDTOValidation(ASPContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull()
                               .WithMessage("Gender name is required")
                               .MinimumLength(3)
                               .WithMessage("Min number of characters is 3")
                               .Must((dto, name) => !_context.Genders.Any(g => g.Name == name && g.Id != dto.Id))
                               .WithMessage("Category name is in use");

        }
    }
}
