using Application.DTO.Cart;
using Application.DTO.User;
using Application.UseCases.Queries;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetOneUser : EfUseCase,IGetOneUser
    {
        public EfGetOneUser(ASPContext context) : base(context)
        {
        }

        public int Id => 31;

        public string Name => this.GetType().Name;

        public UserOneGetDTO Execute(int search_id)
        {
            var user_obj = Context.Users.Find(search_id);
            if (user_obj == null)
            {
                throw new InvalidOperationException();
            }
            return new UserOneGetDTO
            {
                FirstName = user_obj.First_Name,
                LastName = user_obj.Last_Name,
                Email = user_obj.Email,
                PicturePath=user_obj.Picture,
                CartUser=user_obj.Carts.Select(y=>new CartDTOUser
                {
                    CreatedAt=y.CreatedAt,
                    ProductsCart=y.Product_Carts.Select(z=>new ProductCartUserDTO
                    {
                       
                        ProductName=z.Product.Name,
                        Quantity=z.Quantity
                    })
                })
            };
        }
    }
}
