public class Trefle : Plante
{
    public Trefle()
    {
        Nature = "Plante chanceuse";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 12;
        PrixDeVente = 10;
        PlaceNecessaire=2;
        TerrainPrefere = "Terre Brune";
        BesoinHumidite = 1;
        BesoinTemperature = 15;
        SaisonDePlantaisonPrefere = 1;
        Taille = 1;
        Nom = "un trèfle";
    }
    public override string ToString()
    {
        return base.ToString();
    }
    public override void ChangerTaillePlante(double croissance )
    {
        if (croissance<3)
        {
            this.Taille = 1;
        }
        else if ((croissance>=3)&&(croissance<6))
        {
            this.Taille = 2;

        }
        else if ((croissance>=6)&&(croissance<9))
        {
            this.Taille = 3;
            
        }
        else if (croissance>=9)
        {
            this.Taille = 4;
            
        }
    }
}