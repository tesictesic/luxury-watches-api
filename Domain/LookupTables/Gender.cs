using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LookupTables
{
    public class Gender : LookupName
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}
