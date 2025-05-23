using System.Runtime.InteropServices;

public class Magasin
{
    public int ArgentJoueur {get; private set;}
    public List<Plante> GrainesAchetes {get; private set;}
    public List<Plante> PlantesRecoltes {get; private set;}
    protected List<Plante> PlantesWiki {get; private set;}

    public Magasin(int argentJoueur, List<Plante> wiki)
    {
        GrainesAchetes = new List<Plante>();
        PlantesRecoltes = new List<Plante>();
        ArgentJoueur = argentJoueur;//valeur de base

        PlantesWiki = wiki;
    }

    public override string ToString()
    {
        string affichage="";
        affichage += "\n ---- Bienvenu dans le magasin, vous pouvez acheter des graines ou vendre vos plantes récoltées ici. ----\n\n";
        if (GrainesAchetes.Count >0) //affichage des graines détenues
        {
            affichage += "------ Vous avez déjà des graines : ------ \n";
            foreach(Plante p in GrainesAchetes)
            {
                affichage += $"- {p.Nom} \n";
            }
            affichage += " \n";
        }
        if (PlantesRecoltes.Count>0)
        {
            affichage += "------ Vous avez récolté : ------\n";
            foreach(Plante p in PlantesRecoltes) //affichage des plantes récoltées et donc susceptible d'être vendues
            {
                affichage += $"- {p.Nom}, prix de vente : {p.PrixDeVente} pièces \n";
            }
            affichage += " \n";
        }

        affichage += $"Vous avez {ArgentJoueur} pièces.\n";        
        return affichage;
    }
    
    public string Vendre()
    {
        string affichage="";

        Console.WriteLine("Voici vos possibilités de ventes :");
        int i =0;
        foreach (Plante p in PlantesRecoltes)
        {
            affichage+=$"- {i} : {p.Nom} : {p.PrixDeVente} pièces \n";
            i++;
        }
        Console.WriteLine(affichage);

        Console.WriteLine("\nQuelle plante voulez vous vendre ? Donnez son numéro\n");
        int numeroChoisie = Convert.ToInt32(Console.ReadLine());
        
        if (numeroChoisie < 0 || numeroChoisie > PlantesRecoltes.Count - 1)
        {
            return $"Le numéro n'est pas valide.";
        }

        Plante planteAVendre = PlantesRecoltes[numeroChoisie]; //Pour afficher ensuite le prix de vente

        ArgentJoueur += PlantesRecoltes[numeroChoisie].PrixDeVente;
        PlantesRecoltes.Remove(PlantesRecoltes[numeroChoisie]);
        return  $"La plante a été vendue pour {planteAVendre.PrixDeVente} pièces";
    }

    public Plante VerifierExistencePlante(string planteAcheter, ref bool existence)
    {

        if (planteAcheter == "trefle")
        {
            Trefle planteAAcheter = new Trefle();
            existence= true; 
            return planteAAcheter;
        }
        if (planteAcheter == "ail")
        {
            Ail planteAAcheter = new Ail();
            existence= true;
            return planteAAcheter;
        }
        if (planteAcheter == "bruyere")
        {
            Bruyere planteAAcheter = new Bruyere();
            existence= true;
            return planteAAcheter;
        }
        if (planteAcheter == "bruyere")
        {
            Bruyere planteAAcheter = new Bruyere();
            existence= true;
            return planteAAcheter;
        }
        if (planteAcheter == "drosera")
        {
            Drosera planteAAcheter = new Drosera();
            existence= true;
            return planteAAcheter;
        }
        if (planteAcheter == "iris")
        {
            Iris planteAAcheter = new Iris();
            existence= true;
            return planteAAcheter;
        }
        if (planteAcheter == "jonc")
        {
            Jonc planteAAcheter = new Jonc();
            existence= true;
            return planteAAcheter;
        }
        if (planteAcheter == "trefle")
        {
            Trefle planteAAcheter = new Trefle();
            existence= true;
            return planteAAcheter;
        }
        else
        {
            Plante planteAAcheter = new Trefle(); //Ne sert à rien juste à retourner
            existence = false;
            return planteAAcheter; 
        }

    }

    
    public string Acheter()
    {
        string affichage="";

        Console.WriteLine("Voici vos possibilités d'achats :");
        foreach (Plante p in PlantesWiki)
        {
            affichage+=$"- {p.Nom} : {p.PrixAchatGraine} pièces\n";
        }
        Console.WriteLine(affichage);
        
        Console.WriteLine($"\nQuelle plante voulez vous acheter ? Vous avez {ArgentJoueur} pièces.\n");
        string planteChoisie = Convert.ToString(Console.ReadLine()!);
        
        bool existence = false; //Permet de savoir si une plante a été acheter ou non
        Plante? planteAAcheter = VerifierExistencePlante(planteChoisie, ref existence);
        
        if (existence==true) //La plante existe, a été créer et peut être acheter
        {
            if (ArgentJoueur>=planteAAcheter.PrixAchatGraine)
            {
                ArgentJoueur -= planteAAcheter.PrixAchatGraine;
                GrainesAchetes.Add(planteAAcheter);
                return  $"La plante a été achetée pour {planteAAcheter.PrixAchatGraine} pièces";
            }
            else 
            {
                return "La graine n'a pas pu être achetée";
            }
        }
        else //La plante n'existe pas et n'a pas pu être créé donc achetée
        {
            return "Cette plante n'existe pas. Il faut écrire seulement le nom de la plante, sans majuscule ni accent.";
        }
    }
    

}