﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex);
    }
}
