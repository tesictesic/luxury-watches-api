using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product_Price:Entity
    {
        public int Product_id { get; set; }
        public decimal Price { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual Product Product { get; set; }
    }
}
