using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValhallaManagement.Core.Types;

namespace ValhallaManagement.Core.Entities
{
    public class Vikingo
    {
        [Key]
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int BatallasGanadas { get; private set; }
        public Armas ArmaFavorita { get; private set; }
        public NivelHonor NivelHonor { get; private set; }
        public CausaMuerteGloriosa CausaMuerteGloriosa { get; private set; }
        public int ValhallaPoints { get; private set; }
        public ClasificacionVikingo Clasificacion { get; private set; }
    }
}
