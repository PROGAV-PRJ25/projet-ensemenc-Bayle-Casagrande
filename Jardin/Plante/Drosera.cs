public class Drosera : Plante
{
    public Drosera()
    {
        Nature = "carnivore";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 12;
        PrixDeVente = 13;
        PrixAchatGraine = 3;
        PlaceNecessaire=2;
        TerrainPrefere = "Tourbiere";
        BesoinHumidite = 1;
        BesoinTemperature = 15;
        SaisonDePlantaisonPrefere = 1;
        Taille = 1;
        Nom = "drosera";
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