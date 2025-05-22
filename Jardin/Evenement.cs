using System.Runtime.CompilerServices;

public abstract class Evenement
{
    public string Nom {get; set;}
    protected int ComptMois {get; set;}
    protected int Duree {get; set;}

    public Evenement()
    {
        Nom = "event";
        ComptMois = 0;
        Duree = 0;
    }

    public virtual void Action(Terrain terrain)
    {

    }
   
    
    public override string ToString()
    {
        return $"{Nom} est sur le terrain. \n ";
    }

}