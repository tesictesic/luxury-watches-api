using Application.DTO.Lookup;
using Application.DTO.Product;
using Application.DTO.Searches;
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
    public class EfGetProduct : EfUseCase,IGetProductQuery
    {
        public EfGetProduct(ASPContext context) : base(context)
        {
        }

        public int Id => 24;

        public string Name => this.GetType().Name;

        public PagedResult<ProductGetDTO> Execute(ProductSearchDTO search)
        {
            IQueryable<Product> query=Context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }
            if (search.Color_Id.HasValue)
            {
                query = query.Where(x => x.ProductColors.Any(y => y.Color.Id == search.Color_Id));
            }
            if (search.Brand_Id.HasValue)
            {
                query=query.Where(x=>x.Brand_id==search.Brand_Id);
            }
            if (search.Gender_Id.HasValue)
            {
                query=query.Where(x=>x.Gender_id==search.Gender_Id);
            }
            if(search.Specification_Id.HasValue)
            {
                query = query.Where(x => x.Product_Specifications.Any(y => y.Specification.Id == search.Specification_Id));
            }
            if(search.PriceFrom.HasValue)
            {
                query=query.Where(x=>x.Product_Prices.Any(y=>y.Price>=search.PriceFrom && y.DateTo!=null));
            }
            if(search.PriceTo.HasValue)
            {
                query = query.Where(x => x.Product_Prices.Any(y => y.Price <= search.PriceTo && y.DateTo != null));
            }
            if (!string.IsNullOrEmpty(search.OrderBy))
            {
                if(search.OrderBy=="Name A-Z") {
                    query = query.OrderBy(x => x.Name);
                }
                else if(search.OrderBy=="Name Z-A")
                {
                    query=query.OrderByDescending(query=>query.Name);
                }
                else if(search.OrderBy=="Price lower-high")
                {
                    query = query.OrderBy(x => x.Product_Prices.Select(y => y.Price).First());
                }
                else
                {
                    query = query.OrderByDescending(x => x.Product_Prices.Select(y => y.Price).First());
                }
            }
            
            int pageSize = search.pageSize > 5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data = query.Skip((page - 1) * pageSize)
                    .Take(pageSize).Select(x => new ProductGetDTO
                    {
                        Id = x.Id,
                        ProductName=x.Name,
                        ProductDescription=x.Description,
                        ProductPictureSrc=x.Src,
                        ProductPrice=x.Product_Prices.Select(x=>x.Price).First(),
                        BrandName=x.Brand.Name,
                        GenderName=x.Gender.Name,
                        ProductSpecifications=x.Product_Specifications.Select(x=>new ProductSpecificationDTO
                        {
                            SpecificationId=x.Id,
                            SpecificationValue=x.Value,
                        }),
                        Colors=x.ProductColors.Select(x=>new ColorDTOProduct
                        {
                           Name=x.Color.Name
                        })
                    }).ToList();
            return new PagedResult<ProductGetDTO>
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
