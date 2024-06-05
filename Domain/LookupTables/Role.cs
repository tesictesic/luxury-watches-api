using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LookupTables
{
    public class Role : LookupName
    {
        public virtual ICollection<User> Users { get; set; }
    }
}
