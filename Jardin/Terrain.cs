public class Terrain
{
    public int Capacite {get; set;} // Le jardin a une certaine capacité qui ne peut pas être dépasser
    public int NombreDePlante {get; set;}
    public List<Plante> Plantation {get; set;}
    public Terrain(int placeDisponible)
    {
        NombreDePlante = 0;
        Plantation = new List<Plante>();
        Capacite = 4;
    }
    public override string ToString()
    {
        string affichage ="";
        for (int i=0; i<Plantation.Count(); i++)
        {
            affichage += Plantation[i].ToString();
        }
        return affichage;
    }
    public string Semer(Plante nouvellePlante)
    {
        if (NombreDePlante == Capacite)
        {
            return "Ce terrain n'a plus de place pour acceuillir de nouvelle plante.";
        }
        else 
        {
            NombreDePlante +=1;
            Plantation.Add(nouvellePlante);
            return "La graine a été semé.";
        }
    }
}