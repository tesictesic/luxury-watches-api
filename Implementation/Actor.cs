﻿using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class Actor : IApplicationActor
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<int> AllowedUseCases {get;set;}
    }
    public class UnathorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string FirstName => "unathorized";

        public string LastName => "unathorized";

        public string Email => "/";

        public IEnumerable<int> AllowedUseCases =>new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34};
    }
    
}
