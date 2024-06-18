using Application.DTO;
using Application.UseCases.Commands.UseUserCaseCommands;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.UserUseCases
{
    public class EfCreateUseUserCase : EfUseCase,ICreateUserUseCaseCommand
    {
        private readonly CreateUserUseCaseDTOValidation _validations;
        public EfCreateUseUserCase(CreateUserUseCaseDTOValidation validations, ASPContext context):base(context)
        {
            this._validations = validations;
        }
        public int Id => 12;

        public string Name => this.GetType().Name;

        public void Execute(UserUseCaseDTO data)
        {
            _validations.ValidateAndThrow(data);
            UserUseCase userUseCase = new UserUseCase
            {
                UseCaseId = data.UseCaseId,
                UserId = data.UserId,
            };
            Context.UserUseCases.Add(userUseCase);
            Context.SaveChanges();
            
        }
    }
}
