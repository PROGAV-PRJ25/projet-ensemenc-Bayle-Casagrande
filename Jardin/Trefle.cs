public class Trefle : Plante
{
    public Trefle()
    {
        Nature = "Plante chanceuse";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 12;
        PrixDeVente = 9;
        PrixAchatGraine = 2;
        PlaceNecessaire=2;
        TerrainPrefere = "Terre Brune";
        BesoinHumidite = 60;
        BesoinTemperature = 15;
        SaisonDePlantaisonPrefere = "Printemps";
        Taille = 1;
        Nom = "trefle";
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
        else if ((croissance>=2)&&(croissance<4))
        {
            this.Taille = 2;

        }
        else if ((croissance>=4)&&(croissance<6))
        {
            this.Taille = 3;
            
        }
        else if (croissance>=6)
        {
            this.Taille = 4;
            
        }
    }
}