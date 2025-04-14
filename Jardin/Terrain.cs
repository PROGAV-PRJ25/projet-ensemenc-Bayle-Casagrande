public class Terrain
{
    public int PlaceDisponible {get; set;}
    public int NombreDePlante {get; set;}
    List<Plante> Plantation = new List<Plante>() {get; set;};
    public Terrain(int placeDisponible)
    {
        PlaceDisponible = placeDisponible;
        NombreDePlante = 0;
        Plantation = new List<Plante>();
    }
    public override string ToString()
    {
        string affichage ="";
        for (int i=0; i<=Plantation.Count(); i++)
        {
            affichage += Plantation[i].ToString();
        }
        return affichage;
    }
    public void Planter(Plante nouvellePlante)
    {
        NombreDePlante +=1;
        PlaceDisponible -= nouvellePlante.PlaceNecessaire;
    }
}