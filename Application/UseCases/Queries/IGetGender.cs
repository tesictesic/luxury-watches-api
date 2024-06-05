using Application.DTO;
using Application.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IGetGender:IQuery<GenderSearch, PagedResult<GendersDTO>>
    {
        // propisujemo IEnumerable<GenderDTO>> iz razloga zato sto moze da bude niz vise DTO-a (JSON-a)
    }
}
