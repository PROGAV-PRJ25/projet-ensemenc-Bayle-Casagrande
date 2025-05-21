public class Fee : Evenement
{
    public Fee()
    {
        Nom = "🧚Une fée";
        ComptMois = 0;
        Duree = 2;
    }

    public override void Action(Terrain terEvent)
    {
        ComptMois++;

        if (ComptMois <= Duree)
        {
            terEvent.Fertilite += 0.1;
        }
        else
        {
            terEvent.EventSurTerrain = null;
            terEvent.Fertilite=1;
        }

    }
    
    
}