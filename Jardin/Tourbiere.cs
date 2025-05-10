public class Tourbiere : Terrain
{
    static int numerotation ;
    public Tourbiere(int placeDisponible) : base (placeDisponible)
    {
        Type = "Tourbiere";
        Humidite = 80;
        Temperature = 10;
        numerotation = 1;
    }


}