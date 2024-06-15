using Application.DTO;
using Application.UseCases;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations
{
    public class CreateUserUseCaseDTOValidation:AbstractValidator<UserUseCaseDTO>
    {
        private readonly ASPContext _context;
        public CreateUserUseCaseDTOValidation(ASPContext context)
        {
            this._context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.UserId).NotEmpty()
                               .WithMessage("User cannot be empty")
                               .Must(id => _context.Users.Any(x => x.Id == id))
                               .WithMessage("User must be from table");


            RuleFor(x => x.UseCaseId).NotEmpty()
                                   .WithMessage("UseCase cannot be empty")
                                   .Must(x => x > 0 && x <= UseCaseInfo.MaxUseCaseId)
                                   .WithMessage("Invalid usecase id range.")
                                   .Must((dto, usecaseId) => !_context.UserUseCases.Any(y => y.UseCaseId == usecaseId && y.UserId == dto.UserId))
                                   .WithMessage("That use case has already been set for that user");



        }
       
    }
}
