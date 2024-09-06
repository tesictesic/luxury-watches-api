using Application.DTO.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands.ContactCommands
{
    public interface ICreateContact:ICommand<InsertContactDTO>
    {
    }
}
