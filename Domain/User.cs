using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User:Entity
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
      

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<UserUseCase> UserUseCases { get;set; }


    }
}
