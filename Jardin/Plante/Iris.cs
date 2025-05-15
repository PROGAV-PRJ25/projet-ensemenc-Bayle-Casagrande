public class Iris : Plante
{
    public Iris()
    {
        Nature = "Fleur";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 8;
        PrixDeVente = 15;
        PrixAchatGraine = 4;
        PlaceNecessaire=2;
        TerrainPrefere = "Gleys";
        BesoinHumidite = 1;
        BesoinTemperature = 16;
        SaisonDePlantaisonPrefere = "Ete";
        Taille = 1;
        Nom = "iris";
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