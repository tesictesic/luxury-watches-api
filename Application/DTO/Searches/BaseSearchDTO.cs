using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Searches
{
    public abstract class BaseSearchDTO
    {
        
        public int pageSize { get; set; } = 9;
    }
}
