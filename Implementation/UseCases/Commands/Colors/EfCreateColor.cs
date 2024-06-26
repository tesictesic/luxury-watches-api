﻿using Application.DTO.Lookup;
using Application.UseCases.Commands.ColorCommands;
using DataAcess;
using Domain.LookupTables;
using FluentValidation;
using Implementation.Validations.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Colors
{
    public class EfCreateColor : EfUseCase,ICreateColorCommand
    {
        private readonly CreateColorDTOValidation validations;
        public EfCreateColor(CreateColorDTOValidation validations,ASPContext context):base(context) 
        {
            this.validations=validations;
        }
        public int Id => 10;

        public string Name => this.GetType().Name;

        public void Execute(ColorDTO data)
        {
            this.validations.ValidateAndThrow(data);
            EfLookupTable.Add<Color,ColorDTO>(Context,data);
        }
    }
}
