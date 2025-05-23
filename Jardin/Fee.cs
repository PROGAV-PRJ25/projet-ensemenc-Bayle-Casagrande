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

        if (ComptMois <= Duree) //si son existence est inferieure à sa duree max
        {
            terEvent.Fertilite += 0.1;
        }
        else //arrêt de l'event au bout de 3mois
        {
            terEvent.EventSurTerrain = null;
            terEvent.Fertilite=1;
        }

    }
    
    
}