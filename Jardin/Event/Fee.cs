public class Fee : Evenement
{
    public Fee()
    {
        Nom = "Une fée";
        ComptMois = 0;
        Duree = 2;
    }

    public void Action(Terrain terEvent)
    {
        ComptMois++;

        if (ComptMois<=Duree)
        {
            terEvent.Fertilite += 0.2;

        }
        else
        {
            terEvent.EventSurTerrain.RemoveAt(0);
            terEvent.Event = false;
        }
        
    }
}