﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands.ColorCommands
{
    public interface IDeleteColorCommand:ICommand<DeleteDTO>
    {
    }
}
