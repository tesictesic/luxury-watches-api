using Domain.Join_Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LookupTables
{
    public class Color:LookupName
    {
        public string Class { get; set; }
        public virtual ICollection<Product_Color> ProductColors { get; set; }
    }
}
