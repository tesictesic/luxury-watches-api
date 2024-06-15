using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands.UserCommands;
using DataAcess;
using Domain;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfUserDelete : EfUseCase,IUserDeleteCommand
    {
        public EfUserDelete(ASPContext context) : base(context)
        {
        }

        public int Id => 20;

        public string Name => this.GetType().Name;

        public void Execute(DeleteDTO data)
        {
            User user_obj = Context.Users.Find(data.Id);
            if(user_obj==null) { throw new EntityNotFoundException(nameof(Brand), data.Id); }
            if (user_obj.UserUseCases.Count > 0) { throw new ConflictException("You cannot delete this specification"); }
            Context.Users.Remove(user_obj);
            Context.SaveChanges();
        }
    }
}
