using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface IQuery<TSearch, TResult>:IUseCase
    {
        TResult Execute(TSearch search);

        // ovde imamo da vraca rezultat jer je query komanda. a ima i TSearch. moze umesto TSearch biti i TRequest
    }
}
