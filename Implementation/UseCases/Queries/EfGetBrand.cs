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
    public class EfGetBrand : EfUseCase,IGetBrandQuery
    {
        public EfGetBrand(ASPContext context) : base(context)
        {
        }

        public int Id =>22;

        public string Name => this.GetType().Name;

        public PagedResult<BrandDTO> Execute(LookupSearch search)
        {
            var query = Context.Brands.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            int pageSize = search.pageSize > 5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var data = query.Skip((page - 1) * pageSize)
                     .Take(pageSize).Select(x => new BrandDTO
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description=x.Description
                         
                     }).ToList();
            return new PagedResult<BrandDTO>
            {
                Items = data,
                TotalItems = totalItems,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
}
