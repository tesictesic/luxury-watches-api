using Application.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Logging
{
    public class UseCaseLogger : IUseCaseLogger
    {
        public void Log(UserCaseLog log)
        {
            DateTime date = DateTime.UtcNow;
            string username = log.Username;
            string useCase = log.UseCaseName;
            string useCaseData = JsonConvert.SerializeObject(log.UserCaseData);

            Console.WriteLine($"Date: {date.ToLongDateString()} {date.ToLongTimeString()}, User: {username}, UseCase: {useCase}, Data: {useCaseData}");
        }
    }
}
