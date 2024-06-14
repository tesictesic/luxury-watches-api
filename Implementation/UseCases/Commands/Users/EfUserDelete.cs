using Application.DTO;
using Application.UseCases.Commands.UserCommands;
using DataAcess;
using Domain;
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
            if(user_obj==null) { throw new FileNotFoundException(); }
            if (user_obj.UserUseCases.Count > 0) { throw new ArgumentException("You cannot delete this specification"); }
            Context.Users.Remove(user_obj);
            Context.SaveChanges();
        }
    }
}
