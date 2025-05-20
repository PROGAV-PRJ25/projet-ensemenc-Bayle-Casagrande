public class TerreBrune: Terrain
{


    public TerreBrune(int placeDisponible) : base (placeDisponible)
    {
        Type = "Terre Brune";
        Humidite = 1;
        Temperature = 15;
        Capacite = 5;
        Fertilite = 1.4;
    }


}