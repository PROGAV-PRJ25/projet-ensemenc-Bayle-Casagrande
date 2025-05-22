public class Iris : Plante
{
    public Iris()
    {
        Nature = "Fleur";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 5;
        PrixDeVente = 15;
        PrixAchatGraine = 4;
        PlaceNecessaire=2;
        TerrainPrefere = "Gleys";
        BesoinHumidite = 60;
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
        if (croissance<2)
        {
            this.Taille = 1;
        }
        else if ((croissance>=2)&&(croissance<3))
        {
            this.Taille = 2;

        }
        else if ((croissance>=3)&&(croissance<4))
        {
            this.Taille = 3;
            
        }
        else if (croissance>=4)
        {
            this.Taille = 4;
            
        }
    }
}