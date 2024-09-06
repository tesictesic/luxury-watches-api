using Application.DTO.Product;
using DataAcess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Products
{
    public class UpdateProductDTOValidation:AbstractValidator<ProductDTO>
    {
        private readonly ASPContext _context;
        public UpdateProductDTOValidation(ASPContext conte)
        {
          _context = conte;
            RuleFor(x => x.ProductName).NotEmpty()
                                       .WithMessage("You have to fill in product name")
                                       .MinimumLength(5)
                                       .WithMessage("Minimum length of product name must be 3 char")
                                       .Must((dto,productName) => !_context.Products.Any(c => c.Name == productName && c.Id!=dto.Id))
                                       .WithMessage("Product with that name has already in database");
            RuleFor(x => x.ProductPrice).NotEmpty()
                                       .WithMessage("Your price can't be empty")
                                       .Must((dto, price) => !_context.Products.All(y => y.Product_Prices.Any(y => y.Price == price && y.Product_id != dto.Id)))
                                       .WithMessage("This price for that producut has already been");

            RuleFor(x => x.ProductDescription).NotEmpty()
                                            .WithMessage("You have to fill in product description")
                                            .MinimumLength(30)
                                            .WithMessage("Minimum length of product description must be 30 char");

            //RuleFor(x => x.ProductColors).NotEmpty()
            //                           .WithMessage("You have to choose colors for product")
            //                           .Must(BeAValidColorList)
            //                           .WithMessage("Your colors must be from colors table");
            RuleFor(x => x.BrandId).NotEmpty()
                                  .WithMessage("You have to choose brand for product")
                                  .Must((dto,brand) =>_context.Brands.Any(y => y.Id == brand))
                                  .WithMessage("Your brand has already use");
            RuleFor(x => x.GenderId).NotEmpty()
                                  .WithMessage("You have to choose gender for product")
                                  .Must((dto,gender) => _context.Genders.Any(y => y.Id == gender))
                                  .WithMessage("Your gender is not valid gender");
            //RuleFor(x => x.ProductSpecifications).NotEmpty()
            //                           .WithMessage("You have to choose colors for product")
            //                           .Must(BeAValidSpecificationList)
            //                           .WithMessage("Your colors must be from colors table");
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
