using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LookupTables
{
    public class Brand : LookupName
    {
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
