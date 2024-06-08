using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging
{
    public interface IUseCaseLogger
    {
        void Log(UserCaseLog log);
    }
    public class UserCaseLog
    {
        public string Username { get; set; }
        public object UserCaseData { get; set; }
        public string UseCaseName { get; set; }
    }
}
