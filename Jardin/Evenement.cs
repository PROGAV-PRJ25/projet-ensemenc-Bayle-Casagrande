using System.Runtime.CompilerServices;

public abstract class Evenement
{
    public string Nom {get; set;}
    public int ComptMois {get; set;}
    public int Duree {get; set;}

    public Evenement()
    {
    
    }

    public virtual void Action()
    {

    }
    public override string ToString()
    {

        return $"{Nom} est sur le terrain. \n ";
    }


}