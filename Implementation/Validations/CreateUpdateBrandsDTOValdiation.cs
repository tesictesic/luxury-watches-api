using Application.DTO.Lookup;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations
{
    public class CreateUpdateBrandsDTOValdiation : AbstractValidator<BrandDTO>
    {
        private readonly ASPContext _context;
        public CreateUpdateBrandsDTOValdiation(ASPContext context)
        {

            this._context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull()
                               .WithMessage("Gender name is required")
                               .MinimumLength(3)
                               .WithMessage("Min number of characters is 3")
                               .Must(name => !_context.Brands.Any(g => g.Name == name))
                               .WithMessage("Category name is in use");

            RuleFor(x=>x.Description).NotNull()
                               .WithMessage("Description is required")
                               .MinimumLength(20)
                               .WithMessage("Min number of characters is 20")
                               .Must(description => !_context.Brands.Any(g => g.Description == description))
                               .WithMessage("Description text is in use");

        }
    }
}
