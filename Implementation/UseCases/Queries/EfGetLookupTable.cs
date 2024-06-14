using Application.DTO.Lookup;
using Application.DTO.Searches;
using DataAcess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public static class EfGetLookupTable
    {
        public static PagedResult<LookupDTO> GetPagedResult<T>(DbSet<T> dbSet,LookupSearch search,ASPContext context ) where T : class
        {
            IQueryable<T> query = dbSet.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => EF.Property<string>(x, "Name").ToLower().Contains(search.Keyword.ToLower()));
            }
            int pageSize = search.pageSize > 5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var data = query.Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .Select(x => new LookupDTO
                        {
                            Id = EF.Property<int>(x, "Id"),
                            Name = EF.Property<string>(x, "Name")
                        })
                        .ToList();
            return new PagedResult<LookupDTO>
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
