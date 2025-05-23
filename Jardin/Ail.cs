public class Ail : Plante
{
    public Ail()
    {
        Nature = "légume";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 9; //L'ail vit longtemps et mûrit vite
        PrixDeVente = 8;
        PrixAchatGraine = 2;
        PlaceNecessaire=2;
        TerrainPrefere = "Terre Brune";
        BesoinHumidite = 10;
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
        if (croissance<1)
        {
            this.Taille = 1;
        }
        else if ((croissance>=1)&&(croissance<2))
        {
            this.Taille = 2;

        }
        else if ((croissance>=2)&&(croissance<3))
        {
            this.Taille = 3;
            
        }
        else if (croissance>=3)
        {
            this.Taille = 4;
            
        }
    }
}