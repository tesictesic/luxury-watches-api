using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Join_Tables
{
    public class Product_Color:Entity
    {
        public int Product_id { get; set; }
        public int Color_id { get; set; }

        public Product Product { get; set; }
        public Color Color { get; set; }
    }
}
