using Application.DTO;
using Application.UseCases.Commands.SpecificationCommands;
using DataAcess;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Specifications
{
    public class EfDeleteSpecification : EfUseCase,IDeleteSpecificationCommand
    {
        public EfDeleteSpecification(ASPContext context) : base(context)
        {
        }

        public int Id =>18;

        public string Name => this.GetType().Name;

        public void Execute(DeleteDTO data)
        {
            Specification specification_obj = Context.Specifications.Find(data.Id);
            if (specification_obj == null) { throw new FileNotFoundException(); }
            if (specification_obj.Product_Specifications.Count > 0) { throw new ArgumentException("You cannot delete this specification"); }
            Context.Specifications.Remove(specification_obj);
            Context.SaveChanges();
        }
    }
}
