﻿using Application.DTO.Lookup;
using DataAcess;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations
{
    public class CreateUpdateGenderDTOValidation:AbstractValidator<GenderDTO>
        
    {
        private readonly ASPContext _context;
        public CreateUpdateGenderDTOValidation(ASPContext context)
        {
            this._context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotNull()
                               .WithMessage("Gender name is required")
                               .MinimumLength(3)
                               .WithMessage("Min number of characters is 3")
                               .Must(name => !_context.Genders.Any(g => g.Name == name))
                               .WithMessage("Category name is in use");

        }
    }
}
