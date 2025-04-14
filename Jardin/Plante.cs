abstract class Plante
{
    public string Nature {get; set;}
    public int VitesseDeCroissance {get; set;}
    public int EsperanceDeVie {get; set;}
    public int PrixDeVente {get; set;}
    public int Taille {get; set;}
    public int Age {get; set;}
    int mort;
    public int Mort 
    {
        get {return mort;}
        set{
            if(Age > EsperanceDeVie) 
            {
                mort = 1;
            }
            else
            {
                mort = 0;
            }}}
    public int PlaceNecessaire {get; set;}
    public Terrain TerrainPrefere {get; set;}
    public int BesoinHumidite {get; set;}
    public int BesoinTemperature {get; set;}
    public int SaisonDePlantaison {get; set;}
    public override string ToString()
    {
        string affichage = Nature;
        return affichage;
    }
    void AfficherPlante(Plante plante)
    {
        string[] pousse = new string[4];
        if (plante.Taille==4)
        {
            tpousse[4]  = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = "  ||";
            pousse[1]  = @"\_\/_/";
            pousse[0]  = " _   _";
        }
        else if (plante.Taille==3)
        {
            pousse[4] = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = "  ||";
            pousse[1]  = @"  /\";
            
        }
        else if (plante.Taille==2)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = @"  /\";
        }
        else if (plante.Taille==1)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = @"  /\";
        }
    }
}
    
