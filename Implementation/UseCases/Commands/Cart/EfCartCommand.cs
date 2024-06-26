﻿using Application;
using Application.DTO.Cart;
using Application.UseCases.Commands.CartCommands;
using DataAcess;
using Domain;
using Domain.Join_Tables;
using FluentValidation;
using Implementation.Validations.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Cart
{
    public class EfCartCommand : EfUseCase,ICreateCartCommand
    {
        private readonly CartDTOValidation validations;
        private readonly IApplicationActorProvider actor;

        public EfCartCommand(CartDTOValidation validations,IApplicationActorProvider actorr,ASPContext context) : base(context)
        {
            this.validations= validations;
            this.actor = actorr;
        }

        public int Id => 11;

        public string Name => this.GetType().Name;

        public void Execute(CartDTO data)
        {
            this.validations.ValidateAndThrow(data);
            Domain.Cart cart = new Domain.Cart
            {
                User_id = actor.GetActor().Id

            };
            Context.Carts.Add(cart);
            Context.SaveChanges();

            foreach(ProductCartDTO element in data.ProductCarts)
            {
                Product_Cart product_Cart = new Product_Cart
                {
                    Cart_id = cart.Id,
                    Product_id = element.ProductId,
                    Quantity = element.Quantity,
                };
                Context.Product_Carts.Add(product_Cart);
            }
            Context.SaveChanges();

           
            
        }
    }
}
