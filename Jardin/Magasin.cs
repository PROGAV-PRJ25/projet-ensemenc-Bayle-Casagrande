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
                affichage += $"- {p.Nom} ";
            }
            affichage += " \n";
        }
        if (PlantesRecoltes.Count>0)
        {
            affichage += "------ Vous avez récolté : ------";
            foreach(Plante p in PlantesRecoltes)
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
        int i =1;
        foreach (Plante p in PlantesRecoltes)
        {
            affichage+=$"- {i} : {p.Nom} : {p.PrixDeVente} pièces \n";
            i++;
        }
        Console.WriteLine(affichage);
        Console.WriteLine("\nQuelle plante ? Donnez son numéro\n");
        int numeroChoisie = Convert.ToInt32(Console.ReadLine());
        if (numeroChoisie<1  || numeroChoisie>PlantesRecoltes.Count)
        {
            return $"Le numéro n'est pas valide.";
        }
        Plante planteAVendre = PlantesRecoltes[numeroChoisie];
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
            Plante planteAAcheter = new Trefle(); //Ne sert à rien juste à retourner sua
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

    public void AfficherWiki(Potager potager)
    {
        Console.WriteLine("\nBienvenue dans le wiki !");
        Console.WriteLine("1 - Terrains\n2 - Plantes\n3 - Météo\n4 - Sortir\n");

        string choix = Console.ReadLine()!;

        while ((choix!="1")&&(choix!="2")&&(choix!="3")&&(choix!="4"))
        {
            Console.WriteLine("La saisie n'est pas valide, veuillez recommencer");
            choix = Console.ReadLine()!;
        }

        string affichage = "";
        if (choix=="1")
        {
            foreach (Terrain t in potager.Terrains)
            {
                affichage += $"\n{t.Type} | Humidité : {t.Humidite}% | Temp. : {t.Temperature}°C - Place : {t.Capacite-t.NombreDePlante} - Meteo : {t.Meteo}  \n";
            } 
            Console.WriteLine(affichage);
        }
        else if (choix=="2")
        {
            foreach (Plante p in PlantesWiki)
            {
                affichage += $"\n - {p.Nom} | Vie : {p.EsperanceDeVie} mois | Terrain : {p.TerrainPrefere} | Saison : {p.SaisonDePlantaisonPrefere}  | Vente : {p.PrixDeVente} pièces | Achat : {p.PrixAchatGraine} pièces \n";
            }
            Console.WriteLine(affichage);
        }
        else if (choix=="3")
        {
            Console.WriteLine("\n - Soleil : Temp. +10 \n - Neige : Temp. -15 \n - Pluie : Humidité +30 \n - Vent : Humidité -20");
        }

    }
    
   


}