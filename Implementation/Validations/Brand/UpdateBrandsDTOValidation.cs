using Application.DTO.Lookup;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Brand
{
    public class UpdateBrandsDTOValidation:AbstractValidator<BrandDTO>
    {
        private readonly ASPContext _context;
        public UpdateBrandsDTOValidation(ASPContext context)
        {

            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull()
                               .WithMessage("Gender name is required")
                               .MinimumLength(3)
                               .WithMessage("Min number of characters is 3")
                               .Must((dto,name) => !_context.Brands.Any(g => g.Name == name && g.Id!=dto.Id))
                               .WithMessage("Category name is in use");

            RuleFor(x => x.Description).NotNull()
                               .WithMessage("Description is required")
                               .MinimumLength(20)
                               .WithMessage("Min number of characters is 20")
                               .Must((dto,description) => !_context.Brands.Any(g => g.Description == description && g.Id!=dto.Id))
                               .WithMessage("Description text is in use");

        }
    }
}
