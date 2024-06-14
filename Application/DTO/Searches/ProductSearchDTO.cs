using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Searches
{
    public class ProductSearchDTO:BaseSearchDTO
    {
        public string? Name { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int? Brand_Id { get; set; }
        public int? Gender_Id { get; set; }
        public int? Specification_Id { get; set; }
        public int? Color_Id { get; set; }
        public string? OrderBy { get; set; }

    }
}
