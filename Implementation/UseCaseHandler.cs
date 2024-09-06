using Application;
using Application.Logging;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation
{
    public class UseCaseHandler // sta sve treba da se uradi za svaki slucaj koriscenja
    {
        private IExceptionLogger _logger; // referenca na nadtip(logger greski)
        private IApplicationActorProvider actor;
        private IUseCaseLogger iloger;

        public UseCaseHandler(IExceptionLogger logger, IApplicationActorProvider actor,IUseCaseLogger iloger )
        {
            this._logger = logger;            
            this.actor = actor;
            this.iloger= iloger;
        }
        //vazi za bilo koju komandu, podaci koji ulaze;
        public void HandleCommand<TData>(ICommand<TData> command , TData data)
        {
           CheckActorUsesCases(command);
            
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                command.Execute(data);
                stopwatch.Stop();

                if (actor.GetActor().Id > 0)
                {
                    LoggingUseCase(command, data);
                }
                Console.WriteLine(command.Name+ "Duration:"+ stopwatch.ElapsedMilliseconds+" ms.");
                Console.WriteLine("Actor:"+actor.GetActor().FirstName+" "+actor.GetActor().LastName+" "+actor.GetActor().Email);
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
        public TResult HandleQuery<TResult, TSearch>(IQuery<TSearch, TResult> query, TSearch search)
        {
            CheckActorUsesCases(query);
            try
            {
               
                var stopwatch = new Stopwatch();
                stopwatch.Start();
               var result=query.Execute(search);
                stopwatch.Stop();
                if (actor.GetActor().Id != 0)
                {
                    LoggingUseCase(query, search);
                }
                
                Console.WriteLine(query.Name + "Duration:" + stopwatch.ElapsedMilliseconds + " ms.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
        private void CheckActorUsesCases(IUseCase useCase)
        {
            if (!actor.GetActor().AllowedUseCases.Contains(useCase.Id))
            {
                throw new UnauthorizedAccessException();
            }
        }
        private void LoggingUseCase(IUseCase useCase,object data)
        {
            UserCaseLog log = new UserCaseLog
            {
                Username = actor.GetActor().FirstName + " " + actor.GetActor().LastName,
                UseCaseName = useCase.Name,
                UserCaseData = data
            };
            iloger.Log(log);
        }
    }
}
