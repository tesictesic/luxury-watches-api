using Application;
using Application.Logging;
using DataAcess;
using Domain;
using Implementation.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Logging
{
    public class ExceptionLogger : IExceptionLogger
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExceptionLogger(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        Guid IExceptionLogger.Log(Exception ex)
        {
            Guid id = Guid.NewGuid();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ASPContext>();
                ErrorLog log = new ErrorLog
                {
                    ErrorId = id,
                    Message = ex.Message,
                    StrackTrace = ex.StackTrace,
                    Time = DateTime.UtcNow
                };
                context.ErrorLog.Add(log);
                context.SaveChanges();
            }

            return id;
        }
    }
}
