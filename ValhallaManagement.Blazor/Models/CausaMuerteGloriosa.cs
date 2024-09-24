using System.ComponentModel;


namespace ValhallaManagement.Blazor.Models
{
    public enum CausaMuerteGloriosa
    {

        [Description("Batalla en campo abierto")]
        BatallaCampoAbierto = 1,
        [Description("Duelo uno contra uno")]
        DueloUnoContraUno = 2,
        [Description("Protegiendo su aldea")]
        ProtegiendoAldea = 3
    }
}
