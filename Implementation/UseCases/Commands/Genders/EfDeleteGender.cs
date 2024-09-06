using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands.GenderCommands;
using DataAcess;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Genders
{
    public class EfDeleteGender : EfUseCase,IDeleteGenderCommand
    {
        public EfDeleteGender(ASPContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => this.GetType().Name;

        public void Execute(DeleteDTO data)
        {
            Gender gender_obj = Context.Genders.Find(data.Id);
            if (gender_obj == null) { throw new EntityNotFoundException(nameof(Gender), data.Id); }
            if (gender_obj.Products.Count > 0) { throw new ConflictException("You cannot delete this gender"); }
            Context.Genders.Remove(gender_obj);
            Context.SaveChanges();
        }
    }
}
