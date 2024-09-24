using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValhallaManagement.Blazor.Models;

namespace ValhallaManagement.Blazor.Services
{
    public interface IVikingoService
    {
        // Guardar un nuevo vikingo o actualizar uno existente
        Task SaveVikingo(VikingoFormModel model);
        // Obtener un vikingo por su ID
        Task<VikingoFormModel> GetVikingoById(int id);

        // Obtener la lista de todos los vikingos
        Task<List<VikingoFormModel>> GetAllVikingos();

        // Editar un vikingo existente
        Task UpdateVikingo(int id,VikingoFormModel model);

        // Eliminar un vikingo por su ID
        Task DeleteVikingo(int id);
    }
}
