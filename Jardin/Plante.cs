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
}
    
