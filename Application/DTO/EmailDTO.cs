using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class EmailDTO
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SendTo { get; set; }

    }
}
