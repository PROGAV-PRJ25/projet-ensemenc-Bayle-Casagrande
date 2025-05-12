using System.Runtime.InteropServices;

public class Magasin
{
    public int ArgentJoueur {get; set;}
    public List<Plante> GrainesAchetes {get; set;}
    public List<Plante> PlantesRecoltes {get;set;}
    public List<Plante> PlantesWiki {get; set;}

    public Magasin(int argentJoueur)
    {
        GrainesAchetes = new List<Plante>();
        PlantesRecoltes = new List<Plante>();
        ArgentJoueur = argentJoueur;
        Plante ail = new Ail();
        Plante bruyere = new Bruyere();
        Plante drosera = new Drosera();
        Plante iris = new Iris();
        Plante jonc = new Jonc();
        Plante trefle = new Trefle();
        PlantesWiki = new List<Plante>(){ail,bruyere,drosera,iris,jonc,trefle};
    }

    public override string ToString()
    {
        string affichage="";
        affichage += "\nBienvenu dans le magasin, vous pouvez acheter des graines ou vendre vos plantes récoltées ici. \n";
        if (GrainesAchetes.Count >0)
        {
            affichage += "------ Vous avez déjà des graines : ------ ";
            foreach(Plante p in GrainesAchetes)
            {
                affichage += $"- {p.Nom}";
            }
            affichage += " \n";
        }
        if (PlantesRecoltes.Count>0)
        {
            affichage += "------ Vous avez récolté : ------";
            foreach(Plante p in PlantesRecoltes)
            {
                affichage += $"- {p.Nom}, prix de vente à {p.PrixDeVente} pièces";
            }
            affichage += " \n";
        }

        // Mettre les possibilité d'achat
        affichage += $"\nVous avez {ArgentJoueur} pièces.";        
        return affichage;
    }
    
    public string Vendre(int numero)
    {
        Plante planteAVendre = PlantesRecoltes.FirstOrDefault(p => p.Numero == numero);
        if (planteAVendre == null)
        {
            return $"Aucune plante trouvée avec le numéro {numero}.";
        }
        if (planteAVendre.Taille==4 && planteAVendre.Mort==0)
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
        else
        {
            Plante planteAAcheter = new Trefle();
            existence = false;
            return planteAAcheter;
        }

    }

    
    public string Acheter(string planteAcheter)
    {
        string affichage="";
        Console.WriteLine("Voici vos possibilités d'achats :");
        foreach (Plante p in PlantesWiki)
        {
            affichage+=$"-{p.Nom}";
        }
        Console.WriteLine(affichage);
        
        bool existence = false;
        Plante planteAAcheter = VerifierExistancePlante(planteAcheter, ref existence);
        Console.WriteLine(planteAAcheter);
        
        if (existence==true)
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
        else
        {
            return "Cette plante n'existe pas. Il faut écrire seulement le nom de la plante, sans majuscule ni accent.";
        }
    }

    public void AfficherWiki(Potager potager)
    {
        Console.WriteLine("\nBienvenue dans le wiki !");
        Console.WriteLine("1-Terrains\n2-Plantes\n3-Sortir\n");

        string choix = Console.ReadLine()!;

        while ((choix!="1")&&(choix!="2")&&(choix!="3"))
        {
            Console.WriteLine("La saisie n'est pas valide, veuillez recommencer");
            choix = Console.ReadLine()!;
        }

        string affichage = "";
        if (choix=="1")
        {
            foreach (Terrain t in potager.Terrains)
            {
                affichage += $"{t.Type} - {t.Humidite} Humidité - {t.Temperature} Température\n";
            } 
            Console.WriteLine(affichage);
        }
        else if (choix=="2")
        {
            foreach (Plante p in PlantesWiki)
            {
                affichage += $"- {p.Nom} - {p.EsperanceDeVie} mois Espérance de vie - {p.TerrainPrefere} terrain préféré - {p.SaisonDePlantaisonPrefere} saison préférée - {p.PrixDeVente} prix de vente - {p.PrixAchatGraine} prix d'achat\n";
            }
            Console.WriteLine(affichage);
        }

    }
    
   


}