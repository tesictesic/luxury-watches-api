using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Searches
{
    public abstract class BaseSearchDTO
    {
        public string? Keyword { get; set; }
        public int pageSize { get; set; } = 5;
    }
}
