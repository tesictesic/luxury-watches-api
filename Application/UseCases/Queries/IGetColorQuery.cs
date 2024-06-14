using Application.DTO.Lookup;
using Application.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IGetColorQuery:IQuery<LookupSearch,PagedResult<LookupDTO>>
    {
    }
}
