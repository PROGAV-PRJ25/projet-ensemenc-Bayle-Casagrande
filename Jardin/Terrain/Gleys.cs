public class Gleys: Terrain
{

    static int numerotation;
    public Gleys(int placeDisponible) : base (placeDisponible)
    {
        Type = "Gleys";
        Humidite = 50;
        Temperature = 15;
        numerotation = 1;
    }

}