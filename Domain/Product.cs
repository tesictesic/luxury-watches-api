using Domain.Join_Tables;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product:LookupName
    {
        public string Src { get; set; }
        public string Description { get; set; }
        public int Brand_id { get; set; }
        public int Gender_id { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Product_Price> Product_Prices { get; set; }
        public virtual ICollection<Product_Color> ProductColors { get; set; }
        public virtual ICollection<Product_Specification> Product_Specifications { get; set; }
        public virtual ICollection<Product_Cart> Product_Carts { get; set;}
        
    }
}
