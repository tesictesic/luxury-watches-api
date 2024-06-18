using Application.DTO.User;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Implementation.Validations.User
{
    public class UpdateUserDTOValidation : AbstractValidator<RegisterDTO>
    {
        public UpdateUserDTOValidation(ASPContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email must not be empty")
          .EmailAddress().WithMessage("Email must be in email format")
          .Must((dto, email) => !context.Users.Any(u => u.Email == email && u.Id != dto.Id))
          .WithMessage("Email is already in use.");



            RuleFor(x => x.First_Name).NotEmpty().MinimumLength(3).WithMessage("First name need to have just 3 character or more");
            RuleFor(x => x.Last_Name).NotEmpty().MinimumLength(3).WithMessage("Last name need to have just 3 character or more");

            RuleFor(x => x.Password).NotEmpty().Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$")
                .WithMessage("Minimum eight characters, at least one uppercase letter, one lowercase letter and one number:");
        }
    }
}
