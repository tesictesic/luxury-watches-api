using Application.DTO.Product;
using Application.DTO.Searches;
using Application.DTO.User;
using Application.UseCases;
using Application.UseCases.Queries;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetUser : EfUseCase,IGetUserQuery
    {
        public EfGetUser(ASPContext context) : base(context)
        {
        }

        public int Id => 25;

        public string Name => this.GetType().Name;

        public PagedResult<UserGetDTO> Execute(UserSerachDTO search)
        {
            IQueryable<User> query=Context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query=query.Where(x=>x.First_Name.ToLower().Contains(search.Keyword.ToLower())
                || x.Last_Name.ToLower().Contains(search.Keyword.ToLower())
                || x.Email.ToLower().Contains(search.Keyword.ToLower())
                );
            }
            if (!string.IsNullOrEmpty(search.OrderBy))
            {
                if(search.OrderBy=="Name A-Z")
                {
                    query = query.OrderBy(x => x.First_Name);
                }
                else
                {
                    query = query.OrderByDescending(x => x.First_Name);
                }
            }
            int pageSize = search.pageSize > 5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data = query.Skip((page - 1) * pageSize)
                    .Take(pageSize).Select(x => new UserGetDTO
                    {
                        Id = x.Id,
                        First_Name = x.First_Name,
                        Last_Name = x.Last_Name,
                        Email = x.Email,
                        Password = x.Password,
                        PicturePath = x.Picture,
                        Roles = x.UserUseCases.Select(x => x.UseCaseId),
                        Carts=x.Carts.Select(x=>new UserCartDTO
                        {
                            CartId=x.Id,
                            CartItems=x.Product_Carts.Select(y=>new UserProductCartDTO
                            {
                                ProductName=y.Product.Name,
                                Quantity=y.Quantity
                            }),
                            Date=x.CreatedAt
                        })
                    }).ToList();

            return new PagedResult<UserGetDTO>
            {
                Items = data,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize
            };
        }
    }
}
