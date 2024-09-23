using System.ComponentModel;

namespace ValhallaManagement.Core.Types
{
    public enum NivelHonor
    {
        Bajo,
        Medio,
        Alto
    }

    public enum ClasificacionVikingo
    {
        Desconocido,
        GuerreroValiente,
        HeroeLegendario,
        CampeonDelValhalla,
        VeteranoValeroso
    }

    public enum Armas
    {
        Hacha,
        Espada,
        Martillo
    }

    public enum CausaMuerteGloriosa
    {
        [Description("Batalla en campo abierto")]
        BatallaCampoAbierto,
        [Description("Duelo uno contra uno")]
        DueloUnoContraUno,
        [Description("Protegiendo su aldea")]
        ProtegiendoAldea
    }
}
