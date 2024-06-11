using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Product
{
    public class ProductDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public IFormFile Image { get; set; }
        public decimal ProductPrice { get; set; }

        public int BrandId { get; set; }
        public int GenderId { get; set; }
        public string ProductSpecifications { get; set; }
        public string ProductColors { get; set; }
    }
}
