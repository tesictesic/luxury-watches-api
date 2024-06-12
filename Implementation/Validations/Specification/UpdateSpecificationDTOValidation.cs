using Application.DTO.Lookup;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Specification
{
    public class UpdateSpecificationDTOValidation:AbstractValidator<SpecificationDTO>
    {
        private readonly ASPContext _context;
        public UpdateSpecificationDTOValidation(ASPContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull()
                               .WithMessage("Specification name is required")
                               .MinimumLength(3)
                               .WithMessage("Min number of characters is 3")
                               .Must((dto,name) => !_context.Specifications.Any(g => g.Name == name && g.Id!=dto.Id))
                               .WithMessage("Specification name is in use");


        }
    }
}
