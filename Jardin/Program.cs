

using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
//-------------variables de base-----------
int mois = 0;
int nbTour = 10;
int argentJoueur = 15;

//-------- création des objets--------
Potager potagerIrlandais = new Potager();
Magasin magasin = new Magasin(argentJoueur);
TerreBrune terrainTerreBrune = new TerreBrune();
Tourbiere terrainTourbiere = new Tourbiere();
Gleys terrainGleys = new Gleys();
potagerIrlandais.AjouterTerrain(terrainGleys);
potagerIrlandais.AjouterTerrain(terrainTerreBrune);
potagerIrlandais.AjouterTerrain(terrainTourbiere);

//---------programme principal structure-------------------------------

//phase d'introduction
PresenterIntroduction(ref nbTour);

//tours
 while (mois < nbTour)
 {
    mois +=1;
    ActiverModeUrgence(potagerIrlandais);
    Console.Clear();
    Console.WriteLine($"\n\n%%%%%%%%% Mois {mois} : {terrainGleys.CalculerSaisonPlantaison(mois)} %%%%%%%%%");
    ChangerClimat(potagerIrlandais,mois);
    ActualiserPlantes(potagerIrlandais);
    ActualiserEvent(potagerIrlandais);
    Console.WriteLine(potagerIrlandais);
    System.Threading.Thread.Sleep(2000);
    FaireActionJoueur(6, magasin, potagerIrlandais, mois);//action joueur + wiki
    System.Threading.Thread.Sleep(1000);

}


Console.WriteLine($"\n\nFin de partie - Vous avez gagné {magasin.ArgentJoueur} pièces. ");
Console.WriteLine(potagerIrlandais);


//-------------------fonctions principales de déroulement de tour--------------

void PresenterIntroduction(ref int nbTour)
{
    Console.Clear();
    Console.WriteLine("🇮🇪 Bienvenu dans le jeu du potager Irlandais ! 🇮🇪\n");
    Console.WriteLine("Règles : ");
    Console.WriteLine("");
    Console.WriteLine("\nDans ce jeu vous pouvez acheter des graines, les planter, puis les faire grandir. \nVous pourrez alors les récolter et les vendre.");
    Console.WriteLine("");
    Console.WriteLine("Chaque plante a des besoins spécifiques. \nTels qu'une saison de plantaison préféré, un terrain préféré.\nMais aussi une température et une humidité qui les maintiennent en vie.\nElles ont aussi besoin d'une certaine place pour grandir sereinement.\n"); 
    Console.WriteLine("");
    Console.WriteLine("Attention ! Des évènements spéciaux peuvent avoir lieu sur vos terrains.\nTels que des fées 🧚 qui augmenteront la fertilité, mais aussi des insectes 🪲 et de la mauvaise herbe 🌿 qui ralentiront la croissance de vos plantes.\n");
    Console.WriteLine("");
    Console.WriteLine(" 🚨 Des urgences peuvent aussi avoir lieu sur vos terrains. \nIl faudra alors vite écrire le mot indiqué pour protéger vos plantes.\nLes souris 🐁 mangent les plantes, tandis que la tempête ⛈️ les abîme. \n");
    Console.WriteLine("");
    Console.WriteLine("Vous avez trois terrains dans votre potager Irlandais, avec chacun des caractéristiques spéciales notamment sur l'humidité et la température.\nCombien de mois souhaitez-vous jouer ?\n");
    nbTour = Convert.ToInt32(Console.ReadLine()!);
    Console.WriteLine($"\nVous avez {nbTour} mois pour utiliser au maximum votre potager Irlandais. Bonne chance ! 🍀\n");
    System.Threading.Thread.Sleep(3000);
    Console.Clear();
     
}

void ActiverModeUrgence(Potager potager)
{
    Random alea = new Random();
    int modeUrgence;
    modeUrgence = alea.Next(0, 5); //une chance sur 5 qu'un des terrains soit touché par le mode Urgence
    int terrainToucher=alea.Next(0,3); //Permet de choisir quel terrain est touché
    if (modeUrgence == 1)
    {
        int typeInt = alea.Next(0,2);
        string type ="";
        if (typeInt==0){type = "Souris";}
        else {type = "Tempête";}
        potager.FaireUrgence(potager.Terrains[terrainToucher], type);
        System.Threading.Thread.Sleep(4000);
    }

}

void ChangerClimat(Potager potager, int temps)
{
    potager.Saison = temps % 4;

    potager.ChangerSaison();

    foreach (Terrain terrain in potager.Terrains)
    {
        terrain.ChangerMeteo();
    }

}

void ActualiserPlantes(Potager potager)
{
    foreach (Terrain terrain in potager.Terrains)
    {
        foreach (Plante plante in terrain.Plantation)
        {
            plante.Age++;
            plante.TomberMalade();
            plante.Pousser(); //fait grandir la plante selon la vitesse de croissance et change son statut de récoltable
        }
    }
}

void ActualiserEvent(Potager potager)
{
    foreach (Terrain terrain in potager.Terrains)
    {
        if (terrain.Event!=true)
        {
            terrain.DeclencherEvent();
        }
        else if (terrain.Event==true)
        {
            foreach (Evenement e in terrain.EventSurTerrain)
            {
                e.Action();
            }
            
        }

    }
}


//-------------------fonctions action joueur--------------

void FaireActionJoueur(int nbAction, Magasin magasin, Potager potager, int temps)
{
    string reponse = "";
    //for (int i = 0;i<nbAction;i++) //Dans le cas ou le nombre d'action serait limiter
    while (reponse !="9")
    {
        Console.WriteLine("\nQue souhaitez-vous faire ?");
        Console.WriteLine("1 - Semer\n2 - Récolter\n3 - Désherber\n4 - Arroser\n5 - Traiter\n6 - Jeter\n7 - Wiki\n8 - Magasin\n9 - Ne rien faire");
        reponse = Console.ReadLine()!; //mettre un vérif de cas
        while ((reponse!="1")&&(reponse!="2")&&(reponse!="3")&&(reponse!="4")&&(reponse!="5")&&(reponse!="6")&&(reponse!="7")&&(reponse!="8")&&(reponse!="9"))
        {
            Console.WriteLine("La saisie est invalide veuillez réessayer");
            reponse = Console.ReadLine()!;
        }
        switch (reponse)
        {
            case "1" :
            ActionSemer(magasin, potager, temps);
            break;

            case "2" :
            ActionRecolter(potager, magasin);
            break;

            case "3" :
            ActionDesherber(potager);
            break;

            case "4" :
            ActionArroser(potager);
            break;

            case "5" :
            ActionTraiter(potager);
            break;

            case "6" :
            ActionJeter(potager);
            break;

            case "7" :
            magasin.AfficherWiki(potager);
            break;
     
            case "8" :
            RentrerMagasin(magasin);
            break;

            case "9" :
            break;

        }
    }
}

void RentrerMagasin(Magasin magasin)
{
    Console.WriteLine(magasin);
    string actionJoueurMagasin = "";
    while (actionJoueurMagasin != "sortir")
    {
        Console.WriteLine("\nVoulez vous 'acheter', 'vendre' ou 'sortir'?\n");
        actionJoueurMagasin =Console.ReadLine()!;
        if (actionJoueurMagasin == "acheter")
        {
        
            Console.WriteLine(magasin.Acheter());
        }
        if (actionJoueurMagasin == "vendre")
        {
            if (magasin.PlantesRecoltes.Count ==0)
            {
                Console.WriteLine("Vous n'avez aucune plante à vendre. Revenez lorsque vous aurez récolté des plantes mûres.");
            }
            else 
            {
                Console.WriteLine(magasin.Vendre());
            }
        }
    }
}

void ActionSemer(Magasin magasin, Potager potager, int temps)
{
    Console.WriteLine("\nChoisissez le numéro d'une graine à semer dans votre inventaire.");

    if (magasin.GrainesAchetes.Count() == 0)
    {
        Console.WriteLine("Aucune graine disponible dans l'inventaire");
    }
    else
    {
        string affichage = "";
        int i = 0;

        foreach (Plante p in magasin.GrainesAchetes)
        {

            affichage += $"{i} - {p.Nom}\n";
            i++;

        }
        Console.WriteLine($"{affichage}");

        int choix = Convert.ToInt32(Console.ReadLine()!);

        while ((choix < 0) || (choix > magasin.GrainesAchetes.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choix = Convert.ToInt32(Console.ReadLine()!);
        }

        Console.WriteLine("\nOù souhaitez-vous la planter ?");
        string listTer = "";
        int j = 0;
        foreach (Terrain t in potager.Terrains)
        {
            listTer += $"\n{j} - {t.Type}";
            j++;
        }
        Console.WriteLine(listTer);

        int choixPlanter = Convert.ToInt32(Console.ReadLine()!);

        while ((choixPlanter < 0) || (choix > potager.Terrains.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choixPlanter = Convert.ToInt32(Console.ReadLine()!);
        }

        Terrain terrainChoisi = potager.Terrains[choixPlanter];
        Plante graineChoisie = magasin.GrainesAchetes[choix];
        Console.WriteLine(terrainChoisi.Semer(graineChoisie, temps));
        magasin.GrainesAchetes.Remove(graineChoisie);
    }
    
}

void ActionRecolter(Potager potager, Magasin magasin)
{
    Console.WriteLine("\nChoisissez une plante mûre à récolter");

    if (potager.PlantesRecoltables.Count()==0)
    {
        Console.WriteLine("Aucune plante n'est mûre dans le potager.");
    }
    else
    {
        string affichage ="";
        int i = 0;

        foreach (Plante p in potager.PlantesRecoltables)
        {
            affichage += $"\n{i} - {p.Nom} - {p.TerrainPlante.Type}";
            i++;
        }
        Console.WriteLine($"{affichage}");

        int choix = Convert.ToInt32(Console.ReadLine()!);
        while ((choix<0)||(choix>potager.PlantesRecoltables.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choix = Convert.ToInt32(Console.ReadLine()!);
        }

        Plante planteChoisie = potager.PlantesRecoltables[choix];
        magasin.PlantesRecoltes.Add(planteChoisie);
        potager.PlantesRecoltables.Remove(planteChoisie); 
        planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);
        planteChoisie.TerrainPlante.NombreDePlante-=1;
        Console.WriteLine("La plante a été récoltée");
    }  
}

void ActionDesherber(Potager potager) 
{
    Console.WriteLine("\nChoisissez le numéro du terrain à désherber ou tapez 'sortir'");

    string affichage = "";
    int k = 0;
    foreach (Terrain t in potager.Terrains)
    {
        if (t.EventSurTerrain.Count == 0)
        {
            affichage += $"\n{k} - {t.Type} - aucun évènement n'a eu lieu sur ce terrain ";
        }
        else
        {
            affichage += $"\n{k} - {t.Type} - {t.EventSurTerrain[0].Nom}";
        }
        k++;
    }
    Console.WriteLine($"{affichage}");
    string choix = Console.ReadLine()!;

    while ((choix!="0")&&(choix!="1")&&(choix!="2")&&(choix!="3"))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer.");
        choix = Console.ReadLine()!;
    }

    Terrain terrainChoisi = potager.Terrains[Convert.ToInt32(choix)];

    if (terrainChoisi.EventSurTerrain.Count == 0)
    {
        Console.WriteLine("Il n'y a pas de mauvaise herbe sur ce terrain");
    }
    else if (terrainChoisi.EventSurTerrain[0].Nom != "De la mauvaise herbe") 
    {
        Console.WriteLine("Il n'y a pas de mauvaise herbe sur ce terrain");
    }
    else
    {
        terrainChoisi.EventSurTerrain.RemoveAt(0);
        terrainChoisi.Event = false;
        Console.WriteLine("Le potager a été désherbé.");
    }
    
}

void ActionArroser(Potager potager)
{
    Console.WriteLine("\nChoisissez le numéro du terrain de la plante à arroser");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{l} - {t.Type}";
        l++;
    }
    Console.WriteLine(affichage);

    string choix1 = Console.ReadLine()!;

    if ((choix1 == "0") || (choix1 == "1") || (choix1 == "2"))
    {
        Terrain terrainChoisi = potager.Terrains[Convert.ToInt32(choix1)];

        Console.WriteLine("\nChoisissez le numéro de la plante à arroser");

        if (terrainChoisi.Plantation.Count() == 0)
        {
            Console.WriteLine("Vous n'avez aucune plante sur ce terrain, revenez lorsque vous en aurez planté.");
        }
        else
        {
            string affichage2 = "";
            int i = 0;
            foreach (Plante p in terrainChoisi.Plantation)
            {
                affichage2 += $"\n{i} - {p.Nom} - {p.Hydratation} Hydratation \n";
                i++;
            }
            Console.WriteLine(affichage2);

            int choix = Convert.ToInt32(Console.ReadLine()!); //faire une vérif aussi

            while ((choix < 0) || (choix > terrainChoisi.Plantation.Count()))
            {
                Console.WriteLine("Saisie incorrecte, veuillez recommencer.");
                choix = Convert.ToInt32(Console.ReadLine()!);
            }
            Plante planteChoisie = potager.Terrains[Convert.ToInt32(choix1)].Plantation[choix];
            planteChoisie.Hydratation += 15;
            Console.WriteLine("La plante a été arrosée");
        }
    }
    else
    {
        Console.WriteLine("Sortie du menu");
    }
}

void ActionTraiter(Potager potager)
{
    Console.WriteLine("\nChoissisez le numéro du terrain de la plante à traiter");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{l} - {t.Type}";
        l++;

    }
    Console.WriteLine(affichage);
    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potager.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }

    Terrain terrainChoisi = potager.Terrains[choix1];

    if (potager.Terrains[choix1].Plantation.Count ==0)
    {
        Console.WriteLine("Vous n'avez aucune plante sur ce terrain, revenez lorsque vous en aurez planté."); 
    }
    else 
    {
        Console.WriteLine("\nChoisissez la plante à traiter");
        string affichage2 = "";
        int i = 0;
        foreach (Plante p in potager.Terrains[choix1].Plantation)
        {
            if (p.Malade==0)
            {
                affichage2 += $"\n{i} - {p.Nom}";
                i++;
            }
            else
            {
                affichage2 += $"\n{i} - {p.Nom} - Malade";
                i++;
            }
            
        }
        Console.WriteLine(affichage2);
        int choix = Convert.ToInt32(Console.ReadLine()!);

        while ((choix<0)||(choix>terrainChoisi.Plantation.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choix = Convert.ToInt32(Console.ReadLine()!);
        }

        Plante planteChoisie = potager.Terrains[choix1].Plantation[choix];

        if (planteChoisie.Malade==1)
        {
            planteChoisie.Malade = 0;
        
            Console.WriteLine("La plante a été traitée, elle n'est plus malade");
        }
        else 
        {
            Console.WriteLine("Cette plante n'est pas malade");
        }
    }
}

void ActionJeter(Potager potager)
{
    Console.WriteLine("\nChoissisez le numéro du terrain de la plante à jeter");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{l} - {t.Type}\n";
        l++;

    }
    Console.WriteLine(affichage);

    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potager.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }
    Terrain terrainChoisi = potager.Terrains[choix1];
    
    if (potager.Terrains[choix1].Plantation.Count == 0)
    {
            Console.WriteLine("Vous n'avez aucune plante sur ce terrain, revenez lorsque vous en aurez planté.");
    }
    else
    {
        Console.WriteLine("\nChoisissez le numéro de la plante à jeter");
        string affichage2 = "";
        int i = 0;
        foreach (Plante p in potager.Terrains[choix1].Plantation)
        {
            if (p.Mort == 1)
                {
                    affichage2 += $"\n{i} - {p.Nom} - Morte\n";
                    i++;
                }
                else
                {
                    affichage2 += $"\n{i} - {p.Nom}\n";
                    i++;
                }
            }
            Console.WriteLine(affichage2);
            int choix = Convert.ToInt32(Console.ReadLine()!);

            while ((choix < 0) || (choix > terrainChoisi.Plantation.Count()))
            {
                Console.WriteLine("Saisie incorrecte, veuillez recommencer");
                choix = Convert.ToInt32(Console.ReadLine()!);
            }

            Plante planteChoisie = potager.Terrains[choix1].Plantation[choix];

            if (planteChoisie.Mort == 1)
            {
                planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);

                Console.WriteLine("La plante morte a été jetée");
            }
            else
            {
                Console.WriteLine("Cette plante est encore en vie, la jeter quand même? (oui/non)");

                string rep = Console.ReadLine()!;

                while ((rep != "oui") && (rep != "non"))
                {
                    Console.WriteLine("Réponse incorrecte, veuillez réessayez");
                    rep = Console.ReadLine()!;
                }

                if (rep == "oui")
                {
                    planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);

                    Console.WriteLine("La plante a été jetée");
                }
                else if (rep == "non")
                {
                    Console.WriteLine("La plante n'a pas été jetée");
                }
            }
        }
}


//-------------------quelques tests réalisé--------------

// %%%%%%%% test de croissance de plante %%%%%%%%
//Trefle test = new Trefle();
//TerreBrune terrain1 = new TerreBrune(4);
//terrain1.Semer(test);

//for (int i = 0; i < 15; i++)
//{
//    test.Pousser();
//    Console.WriteLine(terrain1);
//}



// %%%%%%%% test mode urgence %%%%%%%%

// Trefle plante1 = new Trefle(); //morte
// Trefle plante2 = new Trefle(); //malade
// Trefle plante3 = new Trefle(); //proche mort
// Trefle plante4 = new Trefle(); //mur
// Trefle plante5 = new Trefle(); //normal

// plante1.Mort = 1;
// Console.WriteLine(plante1.Mort);
// plante2.Malade = 1;
// plante3.Compteur = 3;
// plante4.Taille = 4;

// TerreBrune terrain1 = new TerreBrune(4);
// Console.WriteLine(terrain1.Semer(plante1));
// Console.WriteLine(terrain1.Semer(plante2));
// Console.WriteLine(terrain1.Semer(plante3));
// Console.WriteLine(terrain1.Semer(plante4));
// Console.WriteLine(terrain1.Semer(plante5));
// Console.WriteLine(terrain1);

// Potager potager1 = new Potager();
// Console.WriteLine(potager1.AjouterTerrain(terrain1));
// potager1.Urgence(terrain1, "Tempête");



// %%%%%%%% test magasin %%%%%%%%

// Trefle plante1 = new Trefle(); //morte
// Trefle plante2 = new Trefle(); //malade
// Trefle plante3 = new Trefle(); //proche mort
// Trefle plante4 = new Trefle(); //mur
// Trefle plante5 = new Trefle(); //normal

// Magasin magasin = new Magasin(10);

// Console.WriteLine(magasin.Acheter(plante1));
// Console.WriteLine(magasin.Vendre(plante1));
// Console.WriteLine(magasin);

// %%%%%%%% test magasin 2 %%%%%%%%

/*Magasin magasin1 = new Magasin(10);
RentrerMagasin(magasin1);*/


//------Test état plante------

/*Trefle plante1 = new Trefle(); 
Iris plante2 = new Iris(); 
Jonc plante3 = new Jonc(); 
Ail plante6 = new Ail();
Trefle plante7 = new Trefle(); 

Potager potager = new Potager();

TerreBrune terrain1 = new TerreBrune();
Console.WriteLine(terrain1.Semer(plante1,3)); //planter en printemps
Console.WriteLine(terrain1.Semer(plante2,4)); //planter en ete
Console.WriteLine(terrain1.Semer(plante3,8)); //planter en automne
Console.WriteLine(terrain1.Semer(plante6, 11));  //planter en hiver
Console.WriteLine(terrain1.Semer(plante7, 11));  //planter en hiver

Console.WriteLine(potager.AjouterTerrain(terrain1)); 
Console.WriteLine(potager);

//Test compteur
Console.WriteLine(plante1.Compteur); 

//Test du type de terrain bien rempli leur de l'action semer()
Console.WriteLine(plante1.TerrainPlante.Type); 
Console.WriteLine(plante1.TerrainPrefere);  */

