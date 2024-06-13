using Application.DTO;
using Application.UseCases.Commands.ColorCommands;
using DataAcess;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Colors
{
    public class EfDeleteColor : EfUseCase,IDeleteColorCommand
    {
        public EfDeleteColor(ASPContext context) : base(context)
        {
        }

        public int Id => 17;

        public string Name => this.GetType().Name;

        public void Execute(DeleteDTO data)
        {
            Color color_obj = Context.Colors.Find(data.Id);
            if (color_obj == null) { throw new FileNotFoundException(); }
            if (color_obj.ProductColors.Count > 0) { throw new ArgumentException("You cannot delete this color"); }
            Context.Colors.Remove(color_obj);
            Context.SaveChanges();
        }
    }
}
