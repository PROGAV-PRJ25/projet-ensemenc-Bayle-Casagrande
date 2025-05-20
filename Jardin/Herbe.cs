public class Herbe : Evenement
{
    public Herbe()
    {
        Nom = "De la mauvaise herbe";
        ComptMois = 0;
    }

    public void Action(Terrain terEvent)
    {
        ComptMois++;
        terEvent.Acidite=true; 
    }
}