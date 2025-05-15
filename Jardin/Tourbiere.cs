public class Tourbiere : Terrain
{
    public Tourbiere(int placeDisponible) : base (placeDisponible)
    {
        Type = "Tourbiere";
        Humidite = 80;
        Temperature = 20;

    }


}