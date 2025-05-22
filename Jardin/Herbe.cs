public class Herbe : Evenement
{
    public Herbe()
    {
        Nom = "🍃 De la mauvaise herbe";
    }

    public override void Action(Terrain terEvent)
    {
        terEvent.Acidite=true; 
    }
}