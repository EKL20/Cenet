using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValhallaManagement.Core.Types;

namespace ValhallaManagement.Core.Entities
{
    public class Vikingo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get;  set; }
        public int BatallasGanadas { get;  set; }
        public Armas ArmaFavorita { get;  set; }
        public NivelHonor NivelHonor { get;  set; }
        public CausaMuerteGloriosa CausaMuerteGloriosa { get;  set; }
        public int ValhallaPoints { get;  set; }
        public ClasificacionVikingo Clasificacion { get;  set; }
    }
}
