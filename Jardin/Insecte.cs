public class Insecte : Evenement
{
    public Insecte()
    {
        Nom = "🪲 Un insecte";
        ComptMois = 0;
        Duree = 2;
    }

    public override void Action(Terrain terEvent)
    {
        ComptMois++;

        if (ComptMois <= Duree)
        {
            terEvent.Fertilite -= 0.1;

        }
        else
        {
            terEvent.EventSurTerrain = null;
            terEvent.Fertilite = 1;
        }
        
    }
}