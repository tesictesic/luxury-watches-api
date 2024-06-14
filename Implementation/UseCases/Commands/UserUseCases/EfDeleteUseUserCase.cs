using Application.DTO;
using Application.UseCases.Commands.UseUserCaseCommands;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.UserUseCases
{
    public class EfDeleteUseUserCase : EfUseCase,IDeleteUserUseCaseCommand
    {
        public EfDeleteUseUserCase(ASPContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => this.GetType().Name;

        public void Execute(UserUseCaseDTO data)
        {
            UserUseCase userUseCase=Context.UserUseCases.FirstOrDefault(x=>x.UseCaseId==data.UseCaseId&& x.UserId==data.UserId);
            if(userUseCase==null) throw new FileNotFoundException();
            Context.UserUseCases.Remove(userUseCase);
            Context.SaveChanges();
        }
    }
}
