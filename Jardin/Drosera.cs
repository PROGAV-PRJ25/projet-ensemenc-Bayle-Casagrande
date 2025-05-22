public class Drosera : Plante
{
    public Drosera()
    {
        Nature = "carnivore";
        VitesseDeCroissance = 1;
        EsperanceDeVie = 15;
        PrixDeVente = 13;
        PrixAchatGraine = 3;
        PlaceNecessaire=2;
        TerrainPrefere = "Tourbiere";
        BesoinHumidite = 90;
        BesoinTemperature = 15;
        SaisonDePlantaisonPrefere = "Printemps";
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
        else if ((croissance>=4)&&(croissance<7))
        {
            this.Taille = 2;

        }
        else if ((croissance>=7)&&(croissance<11))
        {
            this.Taille = 3;
            
        }
        else if (croissance>=11)
        {
            this.Taille = 4;
            
        }
    }
}