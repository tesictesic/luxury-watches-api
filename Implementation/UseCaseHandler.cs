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
            if (!actor.GetActor().AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedAccessException();
            }
            
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                command.Execute(data);
                stopwatch.Stop();
                UserCaseLog log = new UserCaseLog
                {
                    Username = actor.GetActor().FirstName + " " + actor.GetActor().LastName,
                    UseCaseName = command.Name,
                    UserCaseData = data
                };
                iloger.Log(log);
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
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
               var result=query.Execute(search);
                stopwatch.Stop();
                Console.WriteLine(query.Name + "Duration:" + stopwatch.ElapsedMilliseconds + " ms.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
    }
}
