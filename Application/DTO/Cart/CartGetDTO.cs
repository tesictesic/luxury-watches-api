using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Cart
{
    public class CartGetDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public IEnumerable<UserProductCartDTO> Products { get; set; }
        public DateTime Date { get; set; }
    }
}
