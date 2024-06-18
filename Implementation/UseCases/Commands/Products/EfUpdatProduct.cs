using Application.DTO.Product;
using Application.Exceptions;
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
    public class EfUpdatProduct : EfUseCase,IUpdateProductCommande
    {
        private readonly UpdateProductDTOValidation validation;
        public EfUpdatProduct(UpdateProductDTOValidation validat,ASPContext context) : base(context)
        {
            validation = validat;
        }

        public int Id => 28;

        public string Name => this.GetType().Name;

        public void Execute(ProductDTO data)
        {
            validation.ValidateAndThrow(data);
            Product product = Context.Products.Find(data.Id);
            if (product == null) { throw new EntityNotFoundException(nameof(Product), data.Id); }
            var colors = JsonConvert.DeserializeObject<List<int>>(data.ProductColors);
            // Deserializacija JSON stringa u listu ProductSpecificationDTO objekata
            var Specifications = JsonConvert.DeserializeObject<List<ProductSpecificationDTO>>(data.ProductSpecifications);
            string profile_path = PictureUpload.Upload(data.Image, "products");
            product.Name = data.ProductName;
            product.Description= data.ProductDescription;
            product.Src = profile_path;
            product.Brand_id = data.BrandId;
            product.Gender_id= data.GenderId;
            Product_Price price=Context.Product_Prices.FirstOrDefault(y=>y.Product_id==data.Id);
            price.Price = data.ProductPrice;
            var color = Context.Product_Colors.Where(y => y.Product_id == data.Id).ToList();
            Context.Product_Colors.RemoveRange(color);
            foreach (var item in colors)
            {
                var newColor = new Product_Color
                {
                    Color_id = item,
                    Product_id = data.Id,
                };
                Context.Product_Colors.Add(newColor);
            };
            var old_specifications = Context.Product_Specifications.Where(y => y.Product_id == data.Id).ToList();
            Context.Product_Specifications.RemoveRange(old_specifications);
            foreach(var item2 in Specifications)
            {
                var newSpecification = new Product_Specification
                {
                    Product_id = data.Id,
                    Specification_id = item2.SpecificationId,
                    Value = item2.SpecificationValue
                };
                Context.Product_Specifications.Add(newSpecification);
            }
            Context.SaveChanges();
           

        }
    }
}
