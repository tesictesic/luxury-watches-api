using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AuditLogDTO
    {
        public string UseCaseName { get; set; }
        public string UserName { get; set; }
        public string UserCaseData { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}
