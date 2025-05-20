public class Insecte : Evenement
{
    public Insecte()
    {
        Nom = "Un insecte";
        ComptMois = 0;
        Duree = 3;
    }

    public void Action(Terrain terEvent)
    {
        ComptMois++;

        if (ComptMois<=Duree)
        {
            terEvent.Fertilite -= 0.2;

        }
        else
        {
            terEvent.EventSurTerrain.RemoveAt(0);
            terEvent.Event = false;
        }
        
    }
}