using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Queries;
using DataAcess;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetGender : EfUseCase,IGetGenderQuery
    {
        public EfGetGender(ASPContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => this.GetType().Name;

        public PagedResult<LookupDTO> Execute(GenderSearch search)
        {
            
            IQueryable<Gender> query = Context.Genders.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            int pageSize = search.pageSize>5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data =query.Skip((page - 1) * pageSize)
                     .Take(pageSize).Select(x=>new LookupDTO
                     {
             Id=x.Id,
             Name=x.Name
            }).ToList();

            return new PagedResult<LookupDTO>
            {
                Items=data,
                TotalItems=totalItems,
                CurrentPage=page,
                PageSize=pageSize,
                TotalPages=totalPages
            };
           
        }
    }
}
