using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Cart
{
    public class CartDTO : BaseDTO
    {
        public int UserId { get; set; }
        public IEnumerable<ProductCartDTO> ProductCarts { get; set; }
    }
}
