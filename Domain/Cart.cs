using Domain.Join_Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cart:Entity
    {
        public int User_id { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Product_Cart> Product_Carts { get; set; }
    }
}
