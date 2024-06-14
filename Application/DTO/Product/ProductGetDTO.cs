using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Product
{
    public class ProductGetDTO:BaseDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPictureSrc { get; set; }
        public decimal ProductPrice { get; set; }
        public string BrandName { get; set; }
        public string GenderName { get; set; }
        public IEnumerable<ProductSpecificationDTO> ProductSpecifications { get; set; }
        public IEnumerable<ColorDTOProduct> Colors { get; set; }

    }
    public class ColorDTOProduct
    {
        public string Name { get; set; }
    }
}
