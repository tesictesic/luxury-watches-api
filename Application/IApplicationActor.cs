using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        
        IEnumerable<int> AllowedUseCases { get; } // niz integera gde ce biti prikazani svi aktovi koje korisnik moze da radi
    }
}
