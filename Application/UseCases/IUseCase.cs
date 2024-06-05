using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface IUseCase
    {
        int Id { get; } // na jednostavniji nacin prenosimo podatke, identifikator. ne cuva se ovo u bazi podataka vec u kodu
        string Name { get; } // ime kako se  use case zove (pretraga profila, azuriranje profila, filtriranje proizvoda)...
        //string Description { get; } // opis, nije obavezan.
    }
}
