using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValhallaManagement.Core.Types;

namespace ValhallaManagement.Application.DTOs
{
    public class VikingoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int BatallasGanadas { get; set; }
        public Armas ArmaFavorita { get; set; }
        public NivelHonor NivelHonor { get; set; }
        public CausaMuerteGloriosa CausaMuerteGloriosa { get; set; }
    }
}
