using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public  class GendersDTO : BaseDTO
    {
        public string Name { get; set; }
    }
    public class CreateGenderDTO : GendersDTO
    {
        
    }
    public class UpdateGenderDTO : GendersDTO
    {

    }
}
