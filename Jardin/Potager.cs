using System.ComponentModel;

public class Potager
{
    
    //-------accesseurs----------- 
    public List<Terrain> Terrains { get; set; } //liste des terrains présents dans le potager
    public List<Plante> PlantesRecoltables {get;set;} //liste des plantes récoltables dans le potager
    public int Saison {get; set;} //saison du potager
    public List<Plante> PlantesWiki {get; set;}


    //-----constructeur----
    public Potager()
    {
        PlantesRecoltables = new List<Plante>();
        Terrains = new List<Terrain>();

        //création des plantes pour le wiki
        Plante ail = new Ail();
        Plante bruyere = new Bruyere();
        Plante drosera = new Drosera();
        Plante iris = new Iris();
        Plante jonc = new Jonc();
        Plante trefle = new Trefle();
        PlantesWiki = new List<Plante>() { ail, bruyere, drosera, iris, jonc, trefle };
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
                terrain.Humidite -= 10;
                terrain.Temperature += 5;
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
                terrain.Humidite += 10;
                terrain.Temperature -= 5;
            }
        }
        else if (this.Saison == 4) //hiver
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite += 10;
                terrain.Temperature -= 3;
            }
        }
    }

    public void AfficherWiki()
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
        if (choix == "1")
        {
            foreach (Terrain t in this.Terrains)
            {
                affichage += $"\n{t.Type} | Humidité : {t.Humidite}% | Temp. : {t.Temperature}°C - Place : {t.Capacite - t.NombreDePlante} - Meteo : {t.Meteo}  \n";
            }
            Console.WriteLine(affichage);
        }
        else if (choix == "2")
        {
            foreach (Plante p in PlantesWiki)
            {
                affichage += $"\n - {p.Nom} | Vie : {p.EsperanceDeVie} mois | Terrain : {p.TerrainPrefere} | Saison : {p.SaisonDePlantaisonPrefere}  | Vente : {p.PrixDeVente} pièces | Achat : {p.PrixAchatGraine} pièces \n";
            }
            Console.WriteLine(affichage);
        }
        else if (choix == "3")
        {
            Console.WriteLine("\n - Soleil : Temp. +10 \n - Neige : Temp. -15 \n - Pluie : Humidité +30 \n - Vent : Humidité -20");
        }

    }

    public void FaireUrgence(Terrain terrain, string type) //mode urgence soit souris soit tempête
    {
        int taille = terrain.Capacite;
        string[,] grille = CreerPotagerDynamique(taille, terrain.Plantation.Count); //génère la grille du mode

        if (type == "Souris")
        {
            //Positionnement aléatoire de la souris sur la grille
            Random random = new Random();
            int positionLigne = random.Next(0, taille / 2);
            int positionColonne = random.Next(0, taille / 2);

            //Sens du mouvement de la souris (soit 1 soit -1)
            int sensHorizontale = 1;
            int sensVerticale = 1;

            string? actionJoueur = ""; //Ce que le joueur va ecrire dans la console
            string planteManger = ""; //affichage finale des plantes mangées
            int reussite = 0; //Le joueur ne reussit pas forcement à faire fuir la souris

            Console.Clear();
            Console.WriteLine($"URGENCE : Une souris se déplace dans le terrain {terrain.Type} ! Elle mange toutes vos plantes.");
            Console.WriteLine("Ecrivez 'chasser' dans la console pour la faire fuir !");
            Console.WriteLine("Attention vous ne la ferez peut être pas fuir du premier coup...");
            System.Threading.Thread.Sleep(700);

            while (!(actionJoueur == "chasser" && reussite == 1)) //Le joueur doit ecrire 'chasser' dans la console pour stopper le mouvement de la souris
            {
                Console.Clear();
                Console.WriteLine($"URGENCE : Une souris se déplace dans le terrain {terrain.Type} ! Elle mange toutes vos plantes.");
                Console.WriteLine("Ecrivez 'chasser' dans la console pour le faire fuir !");
                Console.WriteLine("Attention vous ne la ferez peut être pas fuir du premier coup...");

                grille[positionLigne, positionColonne] = null;
                DeplacerSouris(ref positionLigne, ref positionColonne, ref sensHorizontale, ref sensVerticale, taille);

                if (grille[positionLigne, positionColonne] == "🌱​") // si la souris est sur une plante, la plante est mangée donc supprimée
                {
                    int indexPlanteSupprimer = random.Next(0, terrain.Plantation.Count);
                    planteManger += $"{terrain.Plantation[indexPlanteSupprimer]}\n ";
                    terrain.Plantation.RemoveAt(indexPlanteSupprimer);
                }
                grille[positionLigne, positionColonne] = "🐁";
                Console.WriteLine(AfficherPotagerDynamique(taille, grille));

                if (actionJoueur == "chasser" && reussite != 1) //Le joueur ne réussit pas forcement
                {
                    Console.WriteLine("Recommencez !");
                    System.Threading.Thread.Sleep(300);
                    actionJoueur = "";
                }
                System.Threading.Thread.Sleep(700); //Permet de ralentir le mouvement de la souris

                if (Console.KeyAvailable) // Si une touche a été pressée, lire la ligne et remplir le string 'actionJoueur' du mot écrit
                {
                    actionJoueur = Console.ReadLine();
                    reussite = random.Next(0, 2); //Le joueur a une chance sur 2 de reussir à faire fuir la souris
                }
            }

            Console.WriteLine("Bravo ! Vous avez fait fuir la souris 🐁"); //lorsque l'on sort de la boucle, donc quand le joueur à réussit son action
            if (planteManger == "")
            {
                Console.WriteLine("Elle ne vous a rien mangé !");
            }
            else
            {
                Console.WriteLine($"Elle vous a mangé : \n {planteManger}");
            }
        }

        if (type == "Tempête")
        {
            Random random = new Random();

            // Création d'une deuxieme grille, pour afficher la pluie sur le jardin, on conserve ainsi une grille de base
            string[,] grillePluie = new string[taille / 2, taille / 2];
            for (int i = 0; i < taille / 2; i++)
            {
                for (int j = 0; j < taille / 2; j++)
                {
                    grillePluie[i, j] = grille[i, j];
                }
            }

            string? actionJoueur = ""; //De même, contient le mot écrit par le joueur
            int reussite = 0;
            int esperancePerdu = 0; //Les plantes perdent de l'esperance de vie dans la durée ou les plantes ne sont pas protégées
            int perdre = 0; //Les plantes ne perdront pas de l'esperance de vie à chaque tour de boucle

            Console.Clear();
            Console.WriteLine("URGENCE : Une tempête passe sur votre jardin ! Elle endommage l'espérance de vie de vos plantes.");
            Console.WriteLine("Ecrivez 'proteger' dans la console pour placer une bâche sur vos plantes !");
            Console.WriteLine("Attention la bâche n'est pas facile à mettre...");
            System.Threading.Thread.Sleep(500);

            while (!(actionJoueur == "proteger" && reussite == 1)) //Le joueur doit ecrire 'proteger' pour stopper la boucle
            {

                Console.Clear();
                Console.WriteLine("URGENCE : Une tempête passe sur votre jardin. Elle endommage l'espérance de vie de vos plantes !");
                Console.WriteLine("Ecrivez 'proteger' dans la console pour placer une bâche sur vos plantes !");
                Console.WriteLine("Attention la bâche n'est pas facile à mettre...");

                foreach (Plante plante in terrain.Plantation) //Toutes les plantes du terrains peuvent perdre de l'espérance de vie
                {
                    perdre = random.Next(0, 10); // Les plantes ont 1 chance sur 10 de perdre 1 d'espérance de vie à chaque tour de boucle
                    if (perdre == 9)
                    {
                        plante.EsperanceDeVie -= 1;
                        esperancePerdu -= 1;
                    }
                }

                for (int i = 0; i < taille / 2; i++) // affichage des gouttes d'eau sur le terrains pour rendre l'affichage dynamique
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

                if (actionJoueur == "proteger" && reussite != 1) //Vérification de la probabilité de réussite de l'action du joueur
                {
                    Console.WriteLine("Raté... Recommencez !");
                    System.Threading.Thread.Sleep(500);
                    actionJoueur = "";
                }
                System.Threading.Thread.Sleep(500); //Permet d'espacer les chutes de pluie

                // Si une touche a été pressée, lire la ligne 
                if (Console.KeyAvailable)
                {
                    actionJoueur = Console.ReadLine();
                    reussite = random.Next(0, 2); //Le joueur a une chance sur 2 de reussir à faire fuir la souris
                }

                // on remet l'affichage du jardin a zéro grace à la grillePluie créé qui n'est jamais modifiée
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
    
    


//-------------------fonctions utilisées par le mode urgence--------------
    public string AfficherPotagerDynamique(int taille, string[,] grille) //Affichage de la grille du potager dynamique
    {
        string retour = "";
        for (int i = 0; i < taille/2; i++)
        {
            for (int j= 0; j < taille/2; j++)
            {   
                if (grille[j,i] == null) //S'il n'y a rien on affiche de la terre
                {
                    retour += "🟫🟫​​";
                }
                else 
                {
                    retour += $" {grille[j,i]} "; //Sinon on affiche ce qu'il y a (goutte de pluie, souris ou plante)
                }
            }
            retour += "\n";
        }
        return retour;
    }

    public string[,] CreerPotagerDynamique(int taille, int nbPlante) //Création de la grille du potager dynamique
    {
        Random random = new Random();

        // Création de la grille représentant le terrain et ajout aléatoire des plantes
        string [,] grille = new string[taille/2, taille/2];
        int ligne = 0;
        int colonne = 0;
        
        // on positionne 2 plantes par ligne aléatoirement
        for (int i = 0; i < nbPlante; i += 2)
        {
            int nombreAleatoire = random.Next(0, taille / 2);
            colonne = nombreAleatoire;
            grille[colonne, ligne] = "​🌱​";

            while (colonne == nombreAleatoire) //il ne faut pas que la deuxieme plante soit placée au meme endroit
            {
                nombreAleatoire = random.Next(0, taille / 2);
            }

            if ((i + 2) < nbPlante) // dans le cas ou le nbPlante est impair, il faut afficher une seule plante sur la dernière ligne
            {
                colonne = nombreAleatoire;
                grille[colonne, ligne] = "🌱​";
            }

            ligne += 1;
        }
        return grille;
    }

    public void DeplacerSouris(ref int positionLigne, ref int positionColonne, ref int sensHorizontale, ref int sensVerticale, int taille)
    {
        // La souris ce déplace vers le haut ou vers le bas en fonction de sens verticale = 1 ou = -1
        if ((positionColonne + 1 + sensVerticale > 0) && (positionColonne + sensVerticale < taille / 2)) //Il ne faut pas qu'elle sorte des limite du terrain
        {
            positionColonne += sensVerticale;
        }
        // Si ce n'est pas possible on change le sens verticale, elle redescend ou remonte sur la colonne à droite ou à gauche en fonction du sensHorizontale =1 ou =-1
        else
        {
            sensVerticale = sensVerticale * (-1);

            if ((positionLigne + 1 + sensHorizontale > 0) && (positionLigne + sensHorizontale < taille / 2)) // Elle se déplace aussi à droite ou a gauche pour changer de colonne
            {
                positionLigne += sensHorizontale;
            }
            else // Si elle est au bout du terrain à droite ou à gauche
            {
                sensHorizontale = sensHorizontale * (-1);
            }
        }
    }

}