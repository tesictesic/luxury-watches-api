using Application.Logging;
using DataAcess;
using Domain;
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
        private readonly ASPContext context;
        public UseCaseLogger(ASPContext context)
        {
            this.context = context;
        }
        public void Log(UserCaseLog log)
        {

            UserUseCaseLog userUseCaseLog = new UserUseCaseLog
            {
                UseCaseName = log.UseCaseName,
                UserName = log.Username,
                UserCaseData = JsonConvert.SerializeObject(log.UserCaseData),
                ExecutedAt = DateTime.UtcNow,
            };
            context.UserUseCaseLogs.Add(userUseCaseLog);
            context.SaveChanges();

            //    DateTime date = DateTime.UtcNow;
            //    string username = log.Username;
            //    string useCase = log.UseCaseName;
            //    string useCaseData = JsonConvert.SerializeObject(log.UserCaseData);

            //    Console.WriteLine($"Date: {date.ToLongDateString()} {date.ToLongTimeString()}, User: {username}, UseCase: {useCase}, Data: {useCaseData}");
            //}
        }
    }
}
