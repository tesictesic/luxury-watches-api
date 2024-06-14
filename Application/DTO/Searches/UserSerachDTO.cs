using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Searches
{
    public class UserSerachDTO:BaseSearchDTO
    {
        public string? Keyword { get; set; }
        public string? OrderBy { get; set; }
    }
}
