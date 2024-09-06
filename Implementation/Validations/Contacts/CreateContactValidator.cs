using Application.DTO.Contacts;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Contact
{
    public class CreateContactValidator:AbstractValidator<InsertContactDTO>
    {
        
        public CreateContactValidator() {

            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Name can not be empty")
                                .MinimumLength(3)
                                .WithMessage("Name must has minimum 3 characters");
            RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("Email must not be empty")
              .EmailAddress()
              .WithMessage("Email must be in email format");

            RuleFor(x => x.Subject).NotEmpty()
                               .WithMessage("Subject can not be empty")
                               .MinimumLength(3)
                               .WithMessage("Subject must has minimum 3 characters");

            RuleFor(x => x.Body).NotEmpty()
                               .WithMessage("Body can not be empty")
                               .MinimumLength(20)
                               .WithMessage("Subject must has minimum 20 characters");





        }
    }
}
