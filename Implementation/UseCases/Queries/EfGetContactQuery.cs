using Application.DTO.Contacts;
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
    public class EfGetContactQuery : EfUseCase,IGetContactQuery
    {
        public EfGetContactQuery(ASPContext context) : base(context)
        {
        }

        public int Id => 33;

        public string Name => this.GetType().Name;

        public PagedResult<InsertContactDTO> Execute(SearchContactDTO search)
        {
            var query = Context.Contacts.AsQueryable();
            if (search.DateFrom.HasValue)
            {
                query=query.Where(y=>y.CreatedAt>=search.DateFrom.Value);
            }
            if (search.DateTo.HasValue)
            {
                query=query.Where(y=>y.CreatedAt<=search.DateTo.Value);
            }
            int pageSize = search.pageSize > 5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data = query.Skip((page - 1) * pageSize)
                    .Take(pageSize).Select(x => new InsertContactDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Subject = x.Subject,
                        Email = x.Email,
                        Body= x.Body,

                    }).ToList();
            return new PagedResult<InsertContactDTO>
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
