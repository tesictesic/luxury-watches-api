using Application.DTO.Cart;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations.Products
{
    public class CartDTOValidation : AbstractValidator<CartDTO>
    {
        private readonly ASPContext _context;
        public CartDTOValidation(ASPContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ProductCarts)
            .NotEmpty()
            .WithMessage("You cannot buy with an empty cart")
            .Must(cartItems => !cartItems.All(item => _context.Products.Any(p => p.Id == item.Id)))
            .WithMessage("Invalid products")
            .Must(cartItems => cartItems.All(item => item.Quantity > 0))
            .WithMessage("Quantity cannot be zero");
        }
    }
}
