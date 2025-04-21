public abstract class Plante
{
    public string Nature {get; set;} //Comestible ou non
    public int VitesseDeCroissance {get; set;} // Echelle de 1 à 5
    public int EsperanceDeVie {get; set;} // Si cette éspérence de vie est atteinte, la plante est déclaré morte
    public int PrixDeVente {get; set;} 
    public int Taille {get; set;} // 1, 2, 3 ou 4
    public Terrain? TerrainPlante {get; set;} //C'est le terrain ou la plante est semé
    public int Age {get; set;} // Permet de savoir si l'éspérance de vie est dépassé ou non
    int mort; 
    public int Mort // 0 ou 1
    {
        get {return mort;}
        set{
            if((Age > EsperanceDeVie)||(Compteur<3)) 
            {
                mort = 1;
            }
            else
            {
                mort = 0;
            }}}
    public int compteur;
    // Comprit entre 0 et 5
    public int Compteur {
        get{return compteur;} 
        set{
            compteur = 5;
            if (TerrainPlante.Capacite-TerrainPlante.NombreDePlante<PlaceNecessaire) // Le trèfle a besoin d'un espace libre de 1 dans le terrain ou il a été planté
            {
                compteur -=1;
            }
            if (TerrainPlante.Type != TerrainPrefere)
            {
                compteur -=1;
            }
            if (BesoinHumidite != TerrainPlante.Humidite)
            {
                compteur -=1;
            }
            if (BesoinTemperature != TerrainPlante.Temperature)
            {
                compteur -=1;
            }
            if (SaisonDePlantaison!=SaisonDePlantaisonPrefere)
            {
                compteur -=1;
            }

        }} //Il permet de comptabiliser combien de condition de préférence de la plante sont respecté
    public int PlaceNecessaire {get; set;} // Chaque plante a besoin d'une certaine place diponible dans le jardin pour être à l'aise
    public string TerrainPrefere {get; set;}
    public int BesoinHumidite {get; set;} // Echelle de 0 à 5
    public int BesoinTemperature {get; set;} 
    public int SaisonDePlantaisonPrefere {get; set;} // 1:Printemps, 2:Ete, 3:Automne, 4:Hiver Saison ou la plante devrait etre planté
    public int SaisonDePlantaison{get; set;} // 1:Printemps, 2:Ete, 3:Automne, 4:Hiver Saison ou la plante est planté
    public Plante(int taille)
    {
        Taille = taille;
        Age = 0;

    }
    public override string ToString()
    {
        if (Mort == 1)
        {
            return "La plante est morte vous devez la récolter.";
        }
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
    
