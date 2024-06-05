using Domain.Join_Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LookupTables
{
    public class Specification:LookupName
    {
        public virtual ICollection<Product_Specification> Product_Specifications { get; set; }
    }
}
