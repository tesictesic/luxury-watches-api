using Application.DTO;
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
    public class EfGetGender : IGetGender
    {
        private ASPContext _context;
        public EfGetGender(ASPContext context)
        {
            this._context=context;
        }
        public int Id => 1;

        public string Name => this.GetType().Name;

        public PagedResult<GendersDTO> Execute(GenderSearch search)
        {
            
            IQueryable<Gender> query = _context.Genders.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            int pageSize = search.pageSize > 0 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data =query.Skip((page - 1) * pageSize)
                     .Take(pageSize).Select(x=>new GendersDTO
            {
             Id=x.Id,
             Name=x.Name
            }).ToList();

            return new PagedResult<GendersDTO>
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
