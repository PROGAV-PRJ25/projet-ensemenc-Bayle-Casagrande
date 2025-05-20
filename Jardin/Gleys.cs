public class Gleys: Terrain
{

    public Gleys() 
    {
        Type = "Gleys";
        Humidite = 50;
        Temperature = 15;
        Capacite = 10;
    }

    public override string Semer(Plante nouvellePlante, int temps)
    {
        nouvellePlante.Hydratation=90;
        return base.Semer(nouvellePlante, temps);

    }
}