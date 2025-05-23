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

        if (ComptMois <= Duree)// si l'existence de l'insecte est inferieure à sa durée maximale
        {
            terEvent.Fertilite -= 0.1;

        }
        else //enlève l'insecte au bout de la durée déterminée
        {
            terEvent.EventSurTerrain = null;
            terEvent.Fertilite = 1;
        }
        
    }
}