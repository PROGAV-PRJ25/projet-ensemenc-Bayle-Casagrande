public class Magasin
{
    public int ArgentJoueur {get; set;}
    public List<Plante> GrainesAchetes {get; set;}
    public List<Plante> PlantesRecoltes {get;set;}
    public Magasin(int argentJoueur)
    {
        GrainesAchetes = new List<Plante>();
        PlantesRecoltes = new List<Plante>();
        ArgentJoueur = argentJoueur;
    }
    public override string ToString()
    {
        string affichage="";
        affichage += "Bienvenu dans le magasin, vous pourvez acheter des graines ou vendre vos plantes récoltés ici. \n";
        if (GrainesAchetes.Count >0)
        {
            affichage += "Vous avez déja des graines : ";
            foreach(Plante p in GrainesAchetes)
            {
                affichage += $"- {p.Nom}";
            }
            affichage += " \n";
        }
        if (PlantesRecoltes.Count>0)
        {
            affichage += "Vous avez récolté : ";
            foreach(Plante p in PlantesRecoltes)
            {
                affichage += $"- {p.Nom}, prix de vente à {p.PrixDeVente} pièces";
            }
            affichage += " \n";
        }

        // Mettre les possibilité d'achat
        return affichage;
    }
    public string Vendre(Plante planteAVendre)
    {
        if (planteAVendre.Taille==4 && planteAVendre.Mort==0)
        {
            ArgentJoueur += planteAVendre.PrixDeVente;
            PlantesRecoltes.Remove(planteAVendre);
            return  $"La plante a été vendu pour {planteAVendre.PrixDeVente} pièces";
        }
        else 
        {
            return "La plante n'a pas pu être vendu";
        }
    }
    public string Acheter(Plante planteAAcheter)
    {
        if (ArgentJoueur>=planteAAcheter.PrixAchatGraine)
        {
            ArgentJoueur -= planteAAcheter.PrixAchatGraine;
            GrainesAchetes.Add(planteAAcheter);
            return  $"La plante a été acheté pour {planteAAcheter.PrixAchatGraine} pièces";
        }
        else 
        {
            return "La graine n'a pas pu être acheter";
        }
    }

}