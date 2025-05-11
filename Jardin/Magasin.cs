using System.Runtime.InteropServices;

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
            affichage += "Vous avez déjà des graines : ";
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
        Console.WriteLine($"Vous avez {ArgentJoueur} pièces.");
        string reponse = "";
        while (reponse !="oui" && reponse !="non")
        {
            Console.WriteLine ("Voulez vous accéder au wiki pour voir vos possibilités d'achats ?");
            reponse = Console.ReadLine();
        }
        if (reponse == "oui")
        {
            //wiki
        }
        
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
            return  $"La plante a été vendu pour {planteAVendre.PrixDeVente} pièces";
        }
        else 
        {
            return "La plante n'a pas pu être vendu. La plante est soit morte, soit pas encore mûre.";
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
        bool existence = false;
        Plante planteAAcheter = VerifierExistancePlante(planteAcheter, ref existence);
        
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



}