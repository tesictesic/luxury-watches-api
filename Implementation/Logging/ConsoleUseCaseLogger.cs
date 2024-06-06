using Application.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Logging
{
    public class ConsoleUseCaseLogger : IExceptionLogger
    {
        public void Log(Exception ex)
        {
            Console.WriteLine("Occured at:" + DateTime.UtcNow);
            Console.WriteLine(ex.Message);
        }
    }
}
