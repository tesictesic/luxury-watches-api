using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Queries;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetColor : EfUseCase,IGetColorQuery
    {
        public EfGetColor(ASPContext context) : base(context)
        {
        }

        public int Id => 22;

        public string Name => this.GetType().Name;

        public PagedResult<LookupDTO> Execute(LookupSearch search)
        {
            PagedResult<LookupDTO> result = EfGetLookupTable.GetPagedResult(Context.Colors, search, Context);
            return result;
        }
    }
}
