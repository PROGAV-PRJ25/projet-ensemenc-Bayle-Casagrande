public class Gleys: Terrain
{

    public Gleys(int placeDisponible) : base (placeDisponible)
    {
        Type = "Gleys";
        Humidite = 50;
        Temperature = 15;
        Capacite = 10;
    }
        public override string Semer(Plante nouvellePlante, int temps)
    {
        nouvellePlante.Humidite=90;
        return base.Semer();

    }
}