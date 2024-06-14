using Application.DTO;
using Application.DTO.Cart;
using Application.DTO.Searches;
using Application.DTO.User;
using Application.UseCases.Queries;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetCart : EfUseCase,IGetCartQuery
    {
        public EfGetCart(ASPContext context) : base(context)
        {
        }

        public int Id => 26;

        public string Name => this.GetType().Name;

        public PagedResult<CartGetDTO> Execute(CartSearchDTO search)
        {
            IQueryable<Cart> query = Context.Carts.AsQueryable();
            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.CreatedAt >= search.DateFrom);
            }
            if(search.DateTo.HasValue)
            {
                query = query.Where(x => x.CreatedAt <= search.DateFrom);
            }
            int pageSize = search.pageSize > 5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data = query.Skip((page - 1) * pageSize)
                     .Take(pageSize).Select(x => new CartGetDTO
                     {
                         Id = x.Id,
                         User = x.User.First_Name + " " + x.User.Last_Name,
                         Products = x.Product_Carts.Select(y => new UserProductCartDTO
                         {
                             ProductName = y.Product.Name,
                             Quantity = y.Quantity
                         }),
                         Date=x.CreatedAt
                         
                     }).ToList();
            return new PagedResult<CartGetDTO>
            {
                Items = data,
                TotalItems = totalItems,
                PageSize = pageSize,
                TotalPages = totalPages,
                CurrentPage = page,
            };
        }
    }
}
