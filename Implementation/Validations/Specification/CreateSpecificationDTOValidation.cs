﻿using Application.DTO.Lookup;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Specification
{
    public class CreateSpecificationDTOValidation : AbstractValidator<SpecificationDTO>
    {
        private readonly ASPContext _context;
        public CreateSpecificationDTOValidation(ASPContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull()
                               .WithMessage("Specification name is required")
                               .MinimumLength(3)
                               .WithMessage("Min number of characters is 3")
                               .Must(name => !_context.Specifications.Any(g => g.Name == name))
                               .WithMessage("Specification name is in use");


        }
    }
}
