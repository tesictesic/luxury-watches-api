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
    public class EfGetProductSinglePage : EfUseCase,IGetProductSinglePage
    {
        public EfGetProductSinglePage(ASPContext context) : base(context)
        {
        }

        public int Id => 27;

        public string Name => this.GetType().Name;

        public ProductGetDTO Execute(ProductSinglePageDTO search)
        {
            Product product_obj = Context.Products.Find(search.Id);
            if (product_obj == null) { throw new FileNotFoundException(); }
            ProductGetDTO dto = new ProductGetDTO
            {
                Id = product_obj.Id,
                ProductName = product_obj.Name,
                ProductDescription = product_obj.Description,
                ProductPictureSrc = product_obj.Src,
                ProductPrice = product_obj.Product_Prices.Select(x => x.Price).First(),
                BrandName = product_obj.Brand.Name,
                BrandDescription=product_obj.Brand.Description,
                GenderName = product_obj.Gender.Name,
                ProductSpecifications = product_obj.Product_Specifications.Select(x => new ProductSpecificationGetDTO
                {
                    SpecificationName = x.Specification.Name,
                    SpecificationValue = x.Value,
                }),
                Colors = product_obj.ProductColors.Select(x => new ColorDTOProduct
                {
                    Name = x.Color.Name
                })
            };
            return dto;
        }
    }
}
