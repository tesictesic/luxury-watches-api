using Application.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PicturePath { get; set; }
        public  IEnumerable<int> Roles { get; set; }
        public IEnumerable<UserCartDTO> Carts { get; set; }
    }
    public class UserCartDTO
    {
        public int CartId { get; set; }
        public IEnumerable<UserProductCartDTO> CartItems { get; set; }
        public DateTime Date { get; set; }
    }
    public class UserProductCartDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
