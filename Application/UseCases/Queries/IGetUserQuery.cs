using Application.DTO.Searches;
using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IGetUserQuery:IQuery<UserSerachDTO,PagedResult<UserGetDTO>>
    {
    }
}
