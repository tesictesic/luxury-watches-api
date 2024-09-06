using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Product
{
    public class ProductSpecificationDTO
    {

        public int SpecificationId { get; set; }
        public string SpecificationValue { get; set; }
    }
    public class ProductSpecificationGetDTO
    {
        public string SpecificationName { get; set; }
        public string SpecificationValue { get; set; }
    }
}
