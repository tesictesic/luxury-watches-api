﻿using Application.DTO.Cart;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validations
{
    public class CartDTOValidation:AbstractValidator<CartDTO>
    {
        private readonly ASPContext _context;
        public CartDTOValidation(ASPContext context)
        {
            this._context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.UserId).NotEmpty()
                                .WithMessage("User cannot be empty")
                                .NotNull()
                                .WithMessage("User cannot be null")
                                .Must(user => context.Users.Any(y => y.Id == user))
                                .WithMessage("User must be from application");

            RuleFor(x => x.ProductCarts)
            .NotEmpty()
            .WithMessage("You cannot buy with an empty cart")
            .Must(cartItems =>!cartItems.All(item => _context.Products.Any(p => p.Id == item.Id)))
            .WithMessage("Invalid products")
            .Must(cartItems => cartItems.All(item => item.Quantity > 0))
            .WithMessage("Quantity cannot be zero");
        }
    }
}
