using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public abstract class EfUseCase
    {
        private readonly ASPContext _context;
        protected EfUseCase(ASPContext context)
        {
            this._context = context;
        }

        public ASPContext Context => _context;
    }
}
