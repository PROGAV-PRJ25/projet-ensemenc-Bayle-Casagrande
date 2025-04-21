public class Trefle : Plante
{
    public Trefle ()
    {
        this. Nature = "plante chanceuse";
        this.PrixDeVente = 10;

        this.EsperanceDeVie = 12;

        
        this.PlaceNecessaire = 2;
        // this.TerrainPrefere = Terrain; ne fonctionne pas en objet ???
        this.BesoinHumidite = 50;
        this.BesoinTemperature = 15;
        this.SaisonDePlantaison = 1; //== printemps
        
        
    }


    public override void ChangerEtatPlante(double croissance )
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