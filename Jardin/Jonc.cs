public class Jonc : Plante
{
    public Jonc()
    {
        Nature = "herbe";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 14;
        PrixDeVente = 8;
        PrixAchatGraine = 1;
        PlaceNecessaire=2;
        TerrainPrefere = "Gleys";
        BesoinHumidite = 1;
        BesoinTemperature = 13;
        SaisonDePlantaisonPrefere = "Automne";
        Taille = 1;
        Nom = "jonc";
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