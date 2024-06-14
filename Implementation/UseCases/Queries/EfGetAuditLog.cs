using Application.DTO;
using Application.DTO.Searches;
using Application.UseCases.Queries;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetAuditLog : EfUseCase,IGetAuditLogQuery
    {
        public EfGetAuditLog(ASPContext context) : base(context)
        {
        }

        public int Id => 7;

        public string Name => this.GetType().Name;

        public PagedResult<AuditLogDTO> Execute(AuditLogSearchDTO search)
        {
            IQueryable<UserUseCaseLog> query = Context.UserUseCaseLogs.AsQueryable();
            if (!string.IsNullOrEmpty(search.User))
            {
                query=query.Where(x=>x.UserName.ToLower().Contains(search.User.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.UseCaseName))
            {
                query=query.Where(x=>x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }
            if (search.DateFrom.HasValue)
            {
                query = query.Where(x =>x.ExecutedAt>=search.DateFrom);
            }
            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.ExecutedAt <= search.DateTo);
            }
            int pageSize = search.pageSize > 5 ? search.pageSize : 5;
            int totalItems = query.Count();
            int page = 1;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data = query.Skip((page - 1) * pageSize)
                     .Take(pageSize).Select(x => new AuditLogDTO
                     {
                      UseCaseName=x.UseCaseName,
                      UserName=x.UserName,
                      UserCaseData=x.UserCaseData,
                      ExecutedAt=x.ExecutedAt
                     }).ToList();
            return new PagedResult<AuditLogDTO>
            {
                Items = data,
                TotalItems = totalItems,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
}
