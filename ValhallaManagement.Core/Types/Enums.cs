using System.ComponentModel;

namespace ValhallaManagement.Core.Types
{
    public enum NivelHonor
    {
        Bajo= 1,
        Medio = 2,
        Alto =3
    }

    public enum ClasificacionVikingo
    {
        Desconocido = 1,
        GuerreroValiente = 2,
        HeroeLegendario = 3,
        CampeonDelValhalla = 4,
        VeteranoValeroso = 5
    }

    public enum Armas
    {
        Hacha = 1,
        Espada = 2,
        Martillo = 3
    }

    public enum CausaMuerteGloriosa
    {
        [Description("Batalla en campo abierto")]
        BatallaCampoAbierto =1,
        [Description("Duelo uno contra uno")]
        DueloUnoContraUno = 2,
        [Description("Protegiendo su aldea")]
        ProtegiendoAldea = 3
    }
}
