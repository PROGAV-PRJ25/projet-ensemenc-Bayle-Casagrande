using System.Runtime.InteropServices;

public class Magasin
{
    public int ArgentJoueur {get; set;}
    public List<Plante> GrainesAchetes {get; set;}
    public List<Plante> PlantesRecoltes {get;set;}
    public List<Plante> PlantesWiki {get; set;}

    public Magasin(int argentJoueur, List<Plante> wiki)
    {
        GrainesAchetes = new List<Plante>();
        PlantesRecoltes = new List<Plante>();
        ArgentJoueur = argentJoueur;

        PlantesWiki = wiki;
    }

    public override string ToString()
    {
        string affichage="";
        affichage += "\nBienvenu dans le magasin, vous pouvez acheter des graines ou vendre vos plantes récoltées ici. \n";
        if (GrainesAchetes.Count >0) //affichage des graines détenues
        {
            affichage += "------ Vous avez déjà des graines : ------ ";
            foreach(Plante p in GrainesAchetes)
            {
                affichage += $"- {p.Nom} ";
            }
            affichage += " \n";
        }
        if (PlantesRecoltes.Count>0)
        {
            affichage += "------ Vous avez récolté : ------";
            foreach(Plante p in PlantesRecoltes) //affichage des plantes récoltées et donc susceptible d'être vendues
            {
                affichage += $"- {p.Nom}, prix de vente à {p.PrixDeVente} pièces ";
            }
            affichage += " \n";
        }

        // Mettre les possibilité d'achat
        affichage += $"\nVous avez {ArgentJoueur} pièces.";        
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
        Console.WriteLine("\nQuelle plante ? Donnez son numéro\n");
        int numeroChoisie = Convert.ToInt32(Console.ReadLine());
        if (numeroChoisie<0  || numeroChoisie>PlantesRecoltes.Count-1)
        {
            return $"Le numéro n'est pas valide.";
        }
        Plante planteAVendre = PlantesRecoltes[numeroChoisie];
        if (planteAVendre.Taille==4 && planteAVendre.Mort==0) //Cette condition est toujours vérifier, car les plantes sont dans la liste PlantesRecoltables qui verifie deja cela 
        {
            ArgentJoueur += planteAVendre.PrixDeVente;
            PlantesRecoltes.Remove(planteAVendre);
            return  $"La plante a été vendue pour {planteAVendre.PrixDeVente} pièces";
        }
        else 
        {
            return "La plante n'a pas pu être vendue. La plante est soit morte, soit pas encore mûre.";
        }
    }

    public Plante VerifierExistancePlante(string planteAcheter, ref bool existence)
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
            return null; 
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
        
        Console.WriteLine($"\nQuelle plante ? Vous avez {ArgentJoueur} pièces.\n");
        string planteChoisie = Convert.ToString(Console.ReadLine()!);
        
        bool existence = false;
        Plante? planteAAcheter = VerifierExistancePlante(planteChoisie, ref existence);
        
        if (existence==true)
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
        else
        {
            return "Cette plante n'existe pas. Il faut écrire seulement le nom de la plante, sans majuscule ni accent.";
        }
    }

}