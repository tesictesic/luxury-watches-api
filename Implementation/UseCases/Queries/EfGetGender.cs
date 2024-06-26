﻿using Application.DTO.Lookup;
using Application.DTO.Searches;
using Application.UseCases.Queries;
using DataAcess;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetGender : EfUseCase,IGetGenderQuery
    {
        public EfGetGender(ASPContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => this.GetType().Name;

        public PagedResult<LookupDTO> Execute(LookupSearch search)
        {

            PagedResult<LookupDTO> result = EfGetLookupTable.GetPagedResult(Context.Genders, search, Context);
            return result;
           
        }
    }
}
