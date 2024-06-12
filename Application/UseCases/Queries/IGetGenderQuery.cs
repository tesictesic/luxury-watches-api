using Application.DTO.Lookup;
using Application.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IGetGenderQuery:IQuery<GenderSearch, PagedResult<LookupDTO>>
    {
        // propisujemo IEnumerable<GenderDTO>> iz razloga zato sto moze da bude niz vise DTO-a (JSON-a)
    }
}
