public class Ail : Plante
{
    public Ail()
    {
        Nature = "légume";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 9;
        PrixDeVente = 8;
        PrixAchatGraine = 2;
        PlaceNecessaire=2;
        TerrainPrefere = "Terre Brune";
        BesoinHumidite = 1;
        BesoinTemperature = 15;
        SaisonDePlantaisonPrefere = "Hiver";
        Taille = 1;
        Nom = "ail";
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