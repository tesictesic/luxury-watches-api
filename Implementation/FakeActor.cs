using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class FakeActor : IApplicationActorProvider
    {
        public IApplicationActor GetActor()
        {
            return new Actor
            {
                Id = 0,
                FirstName = "Ime_Proba",
                LastName = "Prezime_Proba",
                Email = "Email_Proba",
            };
        }
    }
}
