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
    public class EfGetSpecification : EfUseCase,IGetSpecificationQuery
    {
        public EfGetSpecification(ASPContext context) : base(context)
        {
        }

        public int Id => 23;

        public string Name => this.GetType().Name;

        public PagedResult<LookupDTO> Execute(LookupSearch search)
        {
            PagedResult<LookupDTO> result = EfGetLookupTable.GetPagedResult(Context.Specifications, search, Context);
            return result;
        }
    }
}
