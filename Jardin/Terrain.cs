public class Terrain
{
    public int PlaceDisponible {get; set;}
    public int NombreDePlante {get; set;}
    public Terrain(int placeDisponible)
    {
        PlaceDisponible = placeDisponible;
        NombreDePlante = 0;
    }
    public override string ToString()
    {
        string affichage =" ";
        return affichage;
    }
    public void Planter(Plante nouvellePlante)
    {
        NombreDePlante +=1;
        PlaceDisponible -= nouvellePlante.PlaceNecessaire;
    }
}