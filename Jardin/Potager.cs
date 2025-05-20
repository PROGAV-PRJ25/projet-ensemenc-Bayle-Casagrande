using System.ComponentModel;

public class Potager
{
    
    //-------accesseurs----------- 
    public List<Terrain> Terrains { get; set; } //liste des terrains présents dans le potager
    public List<Plante> PlantesRecoltables {get;set;} //liste des plantes récoltables dans le potager
    public int Saison {get; set;} //saison du potager

    //-----constructeur----
    public Potager()
    {
        PlantesRecoltables = new List<Plante>();
        Terrains = new List<Terrain>();
    }
    
    //------affichage-------
    public override string ToString()
    {
        string affichage = "\n\n----------------- Vos terrains ------------------\n";
        
        foreach (Terrain t in Terrains)
        {
            affichage += $"\n{t.Type} : Capacité {t.Capacite-t.NombreDePlante} - Humidité : {t.Humidite}% - Température : {t.Temperature}°C - Météo : {t.Meteo}\n"; //affichage des conditions du terrain
            System.Threading.Thread.Sleep(500);
            affichage += t.ToString(); //affichage de chaque terrain
            System.Threading.Thread.Sleep(500);
        }
        affichage += "\n------------------------------------------------\n";
        return affichage;
    }

    //--------------méthodes pour le potager--------------

    public string AjouterTerrain(Terrain nouveauTerrain) //msg de retour après l'ajout d'un terrain
    {
        Terrains.Add(nouveauTerrain);
        nouveauTerrain.PotagerTerrain = this;
        return "Le terrain a été ajouté au potager.";
    }

    public void ChangerSaison() //changement des saisons et donc modification de la température et de l'humidité des terrains
    {

        if (this.Saison == 1) //printemps
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite -= 30;
                terrain.Temperature += 11;
            }
        }
        else if (this.Saison == 2) //été
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite -= 20;
                terrain.Temperature += 5;
            }
        }
        else if (this.Saison == 3) //automne
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite += 40;
                terrain.Temperature -= 7;
            }
        }
        else if (this.Saison == 4) //hiver
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite += 10;
                terrain.Temperature -= 9;
            }
        }
    }


    public void FaireUrgence(Terrain terrain, string type) //mode urgence soit souris soit tempête
    {
        int taille = terrain.Capacite;
        string[,] grille = CreerPotagerDynamique(taille, terrain.Plantation.Count); //génère la grille du mode

        if (type == "Souris")
        {
            Random random = new Random();
            int positionLigne = random.Next(0, taille / 2);
            int positionColonne = random.Next(0, taille / 2);
            int sensHorizontale = 1;
            int sensVerticale = 1;
            string? actionJoueur = "";
            string planteManger = "";
            int reussite = 0; //Le joueur ne reussit pas forcement à faire fuir la souris
            Console.Clear();
            Console.WriteLine($"URGENCE : Une souris se déplace dans le terrain {terrain.Type} ! Elle mange toutes vos plantes.");
            Console.WriteLine("Ecrivez 'chasser' dans la console pour le faire fuir !");
            Console.WriteLine("Attention vous ne la ferez peut etre pas fuir du premier coup...");
            System.Threading.Thread.Sleep(700);
            while (!(actionJoueur == "chasser" && reussite == 1))
            {

                Console.Clear();
                Console.WriteLine($"URGENCE : Une souris se déplace dans le terrain {terrain.Type} ! Elle mange toutes vos plantes.");
                Console.WriteLine("Ecrivez 'chasser' dans la console pour le faire fuir !");
                Console.WriteLine("Attention vous ne la ferez peut etre pas fuir du premier coup...");
                grille[positionLigne, positionColonne] = null;
                DeplacerSouris(ref positionLigne, ref positionColonne, ref sensHorizontale, ref sensVerticale, taille);
                if (grille[positionLigne, positionColonne] == "🌱​")
                {
                    int indexPlanteSupprimer = random.Next(0, terrain.Plantation.Count);
                    planteManger += $"{terrain.Plantation[indexPlanteSupprimer]}\n ";
                    terrain.Plantation.RemoveAt(indexPlanteSupprimer);
                }
                grille[positionLigne, positionColonne] = "🐁";
                Console.WriteLine(AfficherPotagerDynamique(taille, grille));
                if (actionJoueur == "chasser" && reussite != 1)
                {
                    Console.WriteLine("Recommencez !");
                    actionJoueur = "";
                }
                System.Threading.Thread.Sleep(500);
                // Si une touche a été pressée, lire la ligne
                if (Console.KeyAvailable)
                {
                    actionJoueur = Console.ReadLine();
                    reussite = random.Next(0, 2); //Le joueur a une chance sur 2 de reussir à faire fuir la souris
                }

            }
            Console.WriteLine("Bravo ! Vous avez fais fuir la souris 🐁");
            if (planteManger == "")
            {
                Console.WriteLine("Elle ne vous a rien manger !");
            }
            else
            {
                Console.WriteLine($"Elle vous a manger : \n {planteManger}");
            }
        }
        if (type == "Tempête")
        {
            Random random = new Random();
            string[,] grillePluie = new string[taille / 2, taille / 2]; //Pour afficher la pluie sur le jardin
            for (int i = 0; i < taille / 2; i++)
            {
                for (int j = 0; j < taille / 2; j++)
                {
                    grillePluie[i, j] = grille[i, j];
                }
            }
            string? actionJoueur = "";
            int reussite = 0;
            int esperancePerdu = 0;
            int perdre = 0; //Les plantes ne perdront pas de l'esperance de vie à chaque tour de boucle
            Console.Clear();
            Console.WriteLine("URGENCE : Une tempête passe sur votre jardin ! Elle endomage l'éspérance de vie de vos plante.");
            Console.WriteLine("Ecrivez 'proteger' dans la console pour placer une bache sur vos plantes !");
            Console.WriteLine("Attention la bache n'est pas facile à mettre...");
            System.Threading.Thread.Sleep(500);
            while (!(actionJoueur == "proteger" && reussite == 1))
            {

                Console.Clear();
                Console.WriteLine("URGENCE : Une tempête passe sur votre jardin. Elle endomage l'éspérance de vie de vos plante !");
                Console.WriteLine("Ecrivez 'proteger' dans la console pour placer une bache sur vos plantes !");
                Console.WriteLine("Attention la bache n'est pas facile à mettre...");
                foreach (Plante plante in terrain.Plantation)
                {
                    perdre = random.Next(0, 10); // Les plantes ont 1 chance sur 10 de perdre 1 d'espérance de vie à chaque tour de boucle
                    if (perdre == 9)
                    {
                        plante.EsperanceDeVie -= 1;
                        esperancePerdu -= 1;
                    }
                }
                for (int i = 0; i < taille / 2; i++)
                {
                    for (int j = 0; j < taille / 2; j++)
                    {
                        int goutte = random.Next(0, 2); // une chance sur 2 pour que la goutte s'affiche sur la case
                        if (goutte == 1)
                        {
                            grillePluie[i, j] = "💧​";
                        }
                    }
                }
                Console.WriteLine(AfficherPotagerDynamique(taille, grillePluie));
                if (actionJoueur == "proteger" && reussite != 1)
                {
                    Console.WriteLine("Raté... Recommencez !");
                    actionJoueur = "";
                }
                System.Threading.Thread.Sleep(500);
                // Si une touche a été pressée, lire la ligne 
                if (Console.KeyAvailable)
                {
                    actionJoueur = Console.ReadLine();
                    reussite = random.Next(0, 2); //Le joueur a une chance sur 2 de reussir à faire fuir la souris
                }
                // on remet l'affichage du jardin a zéro
                for (int i = 0; i < taille / 2; i++)
                {
                    for (int j = 0; j < taille / 2; j++)
                    {
                        grillePluie[i, j] = grille[i, j];
                    }
                }
            }
            Console.WriteLine("Bravo ! Vous avez installé la bache.");
            Console.WriteLine("Mais certaines plantes ont perdu de l'espérance de vie...");
        }
    }
    
    


//-------------------fonctions utiliser par le mode urgence--------------
    public string AfficherPotagerDynamique(int taille, string[,] grille) //grille du potager
    {
        string retour = "";
        for (int i = 0; i < taille/2; i++)
        {
            for (int j= 0; j < taille/2; j++)
            {   
                if (grille[j,i] == null)
                {
                    retour += "🟫🟫​​";
                }
                else 
                {
                    retour += $" {grille[j,i]} ";
                }
            }
            retour += "\n";
        }
        return retour;
    }

    public string[,] CreerPotagerDynamique(int taille, int nbPlante)
    {
        Random random = new Random();
        // Création de la grille représentant le terrain et ajout aléatoire des plantes
        // 2 plantes par ligne positionnées aléatoirement
        string [,] grille = new string[taille/2, taille/2];
        int ligne = 0;
        int colonne = 0;
        for(int i=0; i<nbPlante; i+=2)
        {
            int nombreAleatoire = random.Next(0,taille/2);
            colonne = nombreAleatoire;
            grille [colonne, ligne] = "​🌱​";
            while (colonne == nombreAleatoire) //il ne faut pas que la deuxieme plante soit placé au meme endroit
            {
                nombreAleatoire = random.Next(0,taille/2);
            }
            if ((i+2)<nbPlante) // dans le cas ou le nbPlante est impair
            {
                colonne = nombreAleatoire;
                grille [colonne, ligne] = "🌱​";
            }
            ligne +=1;
        }
        return grille;
    }

    public void DeplacerSouris(ref int positionLigne, ref int positionColonne, ref int sensHorizontale, ref int sensVerticale, int taille)
    {
        if((positionColonne+1+sensVerticale > 0)&&(positionColonne+sensVerticale<taille/2))
        {
            positionColonne += sensVerticale;
        }
        else
        {
            sensVerticale = sensVerticale*(-1);
            if((positionLigne+1+sensHorizontale > 0)&&(positionLigne+sensHorizontale<taille/2))
            {
                positionLigne += sensHorizontale;
            }
            else
            {
                sensHorizontale = sensHorizontale*(-1);
            }  
        }
    }

}