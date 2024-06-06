using Application.DTO;
using DataAcess;
using Domain;
using Domain.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.UseCases.Commands
{
    public static class EfLookupTable
    {
        public static void Add<T,TDto>(ASPContext context, TDto data) 
            where T : LookupName,new()
            where TDto:LookupDTO
            {

            if (typeof(T) == typeof(Brand))
            {
                var brandDTO=(BrandDTO)(object)data;
                Brand tip2 = new Brand
                {
                    Name = brandDTO.Name,
                   Description = brandDTO.Description,
                };
                context.Brands.Add(tip2);
                context.SaveChanges();

                return;
            }
             T tip = new T{
                Name= data.Name,
            };
            context.Set<T>().Add(tip);
            context.SaveChanges();
        }
        public static void Update<T>(ASPContext context, LookupDTO data)
        where T : LookupName
        {
            
            T entity = context.Set<T>().Find(data.Id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID {data.Id} not found.");
            }
            entity.Name = data.Name;

            
            context.SaveChanges();
        }
    }
}
