public class Gleys: Terrain
{

    public Gleys(int placeDisponible) : base (placeDisponible)
    {
        Type = "Gleys";
        Humidite = 50;
        Temperature = 15;
        Capacite = 10;
    }

}