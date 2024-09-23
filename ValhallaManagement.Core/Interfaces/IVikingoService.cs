using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValhallaManagement.Core.Entities;
using ValhallaManagement.Core.Types;

namespace ValhallaManagement.Core.Interfaces
{
    public interface IVikingoService
    {
        int CalcularValhallaPoints(Vikingo vikingo);
        ClasificacionVikingo ClasificarVikingo(Vikingo vikingo);
    }
}
