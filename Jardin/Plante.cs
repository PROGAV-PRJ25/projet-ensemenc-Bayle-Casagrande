public class Plante
{
    public string Nature {get; set;}
    public int VitesseDeCroissance {get; set;}
    public int EsperanceDeVie {get; set;} // Si cette éspérence de vie est atteinte, la plante est déclaré morte
    public int PrixDeVente {get; set;} 
    public int Taille {get; set;} // 1, 2, 3 ou 4
    public int Age {get; set;} // Permet de savoir si l'éspérance de vie est dépassé ou non
    int mort; 
    public int Mort // 0 ou 1
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
    public int PlaceNecessaire {get; set;} // Chaque plante a besoin d'une certaine place diponible dans le jardin pour être à l'aise
    public Terrain TerrainPrefere {get; set;}
    public int BesoinHumidite {get; set;}
    public int BesoinTemperature {get; set;}
    public int SaisonDePlantaison {get; set;}
    public Plante(int taille)
    {
        Taille = taille;
    }
    public override string ToString()
    {
        string[] pousse = AfficherPlante(this);
        string affichage="";
        for(int i=0; i<pousse.Length; i++)
        {
            affichage +=$"{pousse[i]}\n";
        }
        return affichage;
    }
    public string[] AfficherPlante(Plante planteAfficher)
    {
        string[] pousse = new string[5];
        if (planteAfficher.Taille==4)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = "  ||";
            pousse[1]  = @"\_\/_/";
            pousse[0]  = " _   _";
        }
        else if (planteAfficher.Taille==3)
        {
            pousse[4] = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = "  ||";
            pousse[1]  = @"  /\";
            
        }
        else if (planteAfficher.Taille==2)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = @"  /\";
        }
        else if (planteAfficher.Taille==1)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = @"  /\";
        }
        return pousse;
    }
    
}
    
