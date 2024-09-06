using Application.DTO.Product;
using DataAcess;
using Domain.Join_Tables;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Products
{
    public class CreateProductDTOValidation : AbstractValidator<ProductDTO>
    {
        private readonly ASPContext _context;
        public CreateProductDTOValidation(ASPContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ProductName).NotEmpty()
                                        .WithMessage("You have to fill in product name")
                                        .MinimumLength(5)
                                        .WithMessage("Minimum length of product name must be 3 char")
                                        .Must(productName => !_context.Products.Any(c => c.Name == productName))
                                        .WithMessage("Product with that name has already in database");
            RuleFor(x => x.ProductPrice).NotEmpty()
                                       .WithMessage("Your price can't be empty");
            RuleFor(x => x.ProductDescription).NotEmpty()
                                            .WithMessage("You have to fill in product description")
                                            .MinimumLength(30)
                                            .WithMessage("Minimum length of product description must be 30 char");
            //RuleFor(x => x.ProductColors).NotEmpty()
            //                           .WithMessage("You have to choose colors for product");
            //                           //.Must(BeAValidColorList)
            //                           //.WithMessage("Your colors must be from colors table");
            RuleFor(x => x.BrandId).NotEmpty()
                                  .WithMessage("You have to choose brand for product")
                                  .Must(brand => _context.Brands.Any(y => y.Id == brand))
                                  .WithMessage("Your brand is not valid brand");
            RuleFor(x => x.GenderId).NotEmpty()
                                  .WithMessage("You have to choose gender for product")
                                  .Must(brand => _context.Genders.Any(y => y.Id == brand))
                                  .WithMessage("Your gender is not valid gender");
            //RuleFor(x => x.ProductSpecifications).NotEmpty()
            //                           .WithMessage("You have to choose colors for product");
            //                           //.Must(BeAValidSpecificationList)
            //                           //.WithMessage("Your colors must be from colors table");







        }
        private bool BeAValidColorList(string ProductColors)
        {
            if (string.IsNullOrEmpty(ProductColors))
            {
                return false;
            }

            try
            {
                var productColors = JsonConvert.DeserializeObject<List<int>>(ProductColors);

                if (productColors == null || !productColors.Any())
                {
                    return false;
                }

                return productColors.All(colorId => _context.Colors.Any(c => c.Id == colorId));
            }
            catch (JsonException)
            {
                // JSON deserialization failed
                return false;
            }
        }
        private bool BeAValidSpecificationList(string ProductSpecifications)
        {
            if (string.IsNullOrEmpty(ProductSpecifications))
            {
                return false;
            }

            try
            {
                var productSpecifications = JsonConvert.DeserializeObject<List<ProductSpecificationDTO>>(ProductSpecifications);

                if (productSpecifications == null || !productSpecifications.Any())
                {
                    return false;
                }

                return productSpecifications.All(spec => _context.Specifications.Any(s => s.Id == spec.SpecificationId));
            }
            catch (JsonException)
            {
                // JSON deserialization failed
                return false;
            }
        }
    }
}
