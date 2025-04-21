public class Trefle : Plante
{
    public Trefle(int taille) : base (taille)
    {
        Nature = "Non comestible";
        VitesseDeCroissance = 3;
        EsperanceDeVie = 6;
        PrixDeVente = 2;
        PlaceNecessaire=1;
        TerrainPrefere = "Terre Brune";
        BesoinHumidite = 1;
        BesoinTemperature = 15;
        SaisonDePlantaisonPrefere = 2;
    }

}