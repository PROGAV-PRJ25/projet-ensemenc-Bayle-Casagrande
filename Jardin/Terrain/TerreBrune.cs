public class TerreBrune: Terrain
{
    static int numerotation ;

    public TerreBrune(int placeDisponible) : base (placeDisponible)
    {
        Type = "Terres Brune";
        Humidite = 1;
        Temperature = 15;
        numerotation = 1;
    }


}