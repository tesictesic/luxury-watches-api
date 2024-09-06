using Application.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User
{
    public class UserOneGetDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PicturePath { get; set; }
        public IEnumerable<CartDTOUser> CartUser { get; set;}
    }
    public class CartDTOUser
    {
        public DateTime CreatedAt { get; set; }
        public IEnumerable<ProductCartUserDTO> ProductsCart { get; set; }
    }
    public class ProductCartUserDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }


}
