using Application.DTO.Cart;
using Application.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IGetCartQuery:IQuery<CartSearchDTO,PagedResult<CartGetDTO>>
    {
    }
}
