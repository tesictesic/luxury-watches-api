using Application.DTO.Product;
using Application.UseCases.Commands.ProductCommands;
using DataAcess;
using Domain;
using Domain.Join_Tables;
using FluentValidation;
using Implementation.Validations.Products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Products
{
    public class EfCreateProduct : EfUseCase, ICreateProductCommand
    {
        private CreateProductDTOValidation _validation;
        public EfCreateProduct(CreateProductDTOValidation validations, ASPContext context) : base(context)
        {
            this._validation = validations;
        }

        public int Id => 8;

        public string Name => this.GetType().Name;

        public void Execute(ProductDTO data)
        {
            _validation.ValidateAndThrow(data);
            var colors = JsonConvert.DeserializeObject<List<int>>(data.ProductColors);
            // Deserializacija JSON stringa u listu ProductSpecificationDTO objekata
            var Specifications = JsonConvert.DeserializeObject<List<ProductSpecificationDTO>>(data.ProductSpecifications);

            List<string> allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
           
            string profile_path=PictureUpload.Upload(data.Image, "products");
                Product product = new Product
                {
                    Name = data.ProductName,
                    Description = data.ProductDescription,
                    Src =profile_path,
                    Brand_id = data.BrandId,
                    Gender_id = data.GenderId,
                };
                Context.Products.Add(product);
                Context.SaveChanges();
                Product_Price price = new Product_Price
                {
                    Price = data.ProductPrice,
                    Product_id = product.Id,
                };
                Context.Product_Prices.Add(price);
                foreach (int color in colors)
                {
                    Product_Color product_color = new Product_Color
                    {
                        Color_id = color,
                        Product_id = product.Id,
                    };
                Context.Product_Colors.Add(product_color);
            }
                
            foreach (var specification in Specifications)
                {
                    Product_Specification product_Specification = new Product_Specification
                    {
                        Product_id = product.Id,
                        Specification_id = specification.SpecificationId,
                        Value = specification.SpecificationValue,
                    };
                    Context.Product_Specifications.Add(product_Specification);
                }
               Context.SaveChanges();
            }
    }
}
