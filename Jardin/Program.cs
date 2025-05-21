

using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
//-------------variables de base-----------
int mois = 0;
int nbTour = 10;
int argentJoueur = 15;
int choixActionJoueur = -1;
//-------- création des objets--------
Potager potagerIrlandais = new Potager();
Magasin magasin = new Magasin(argentJoueur,potagerIrlandais.PlantesWiki);
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
    System.Threading.Thread.Sleep(5000);
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
    Console.WriteLine("Vous avez trois terrains dans votre potager Irlandais, avec chacun des caractéristiques spéciales notamment sur l'humidité et la température.\n");
    // nbTour = Convert.ToInt32(Console.ReadLine()!);
    nbTour = DemanderAction("Combien de mois souhaitez-vous jouer ?\n",100,1);

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
        if (terrain.EventSurTerrain==null)
        {
            terrain.DeclencherEvent();
        }
        else if (terrain.EventSurTerrain!=null)
        {
            terrain.EventSurTerrain.Action(terrain);
                   
        }

    }
}


//-------------------fonctions action joueur--------------

void FaireActionJoueur(int nbAction, Magasin magasin, Potager potager, int temps)
{
    int reponse=0;

    while (reponse != 9)
    {
        reponse = DemanderAction("\nQue souhaitez-vous faire ?\n1 - Semer\n2 - Récolter\n3 - Désherber\n4 - Arroser\n5 - Traiter\n6 - Jeter\n7 - Wiki\n8 - Magasin\n9 - Ne rien faire", 9, 1);

        switch (reponse)
        {
            case 1:
                ActionSemer(magasin, potager, temps);
                break;

            case 2:
                ActionRecolter(potager, magasin);
                break;

            case 3:
                ActionDesherber(potager);
                break;

            case 4:
                ActionArroser(potager);
                break;

            case 5:
                ActionTraiter(potager);
                break;

            case 6:
                ActionJeter(potager);
                break;

            case 7:
                potager.AfficherWiki();
                break;

            case 8:
                RentrerMagasin(magasin);
                break;

            case 9:
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
        string affichage = ""; //affichage des graines achetées
        int i = 0;
        foreach (Plante p in magasin.GrainesAchetes)
        {

            affichage += $"{i} - {p.Nom}\n";
            i++;

        }
        Console.WriteLine($"{affichage}");


        int choix = DemanderAction("", magasin.GrainesAchetes.Count() - 1, 0); //choix du joueur

        Console.WriteLine("\nOù souhaitez-vous la planter ?"); //affichage des terrains

        AfficherListeTerrains(potagerIrlandais);

        int choixPlanter = DemanderAction("", potager.Terrains.Count() - 1, 0); //choix du joueur

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

        foreach (Plante p in potager.PlantesRecoltables) //affichage des plantes récoltables
        {
            affichage += $"\n{i} - {p.Nom} - {p.TerrainPlante.Type}";
            i++;
        }
        Console.WriteLine($"{affichage}");

        int choix = DemanderAction("",potager.PlantesRecoltables.Count()-1,0);
        
        
        Plante planteChoisie = potager.PlantesRecoltables[choix];
        magasin.PlantesRecoltes.Add(planteChoisie);
        potager.PlantesRecoltables.Remove(planteChoisie);
        planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);
        planteChoisie.TerrainPlante.NombreDePlante -= 1;
        Console.WriteLine("La plante a été récoltée");
        
    }  
}

void ActionDesherber(Potager potager) 
{
    Console.WriteLine("\nChoisissez le numéro du terrain à désherber");

    string affichage = "";
    int k = 0;
    foreach (Terrain t in potager.Terrains) //affichage des terrains 
    {
        if (t.EventSurTerrain == null)
        {
            affichage += $"\n{k} - {t.Type} - aucun évènement n'a eu lieu sur ce terrain ";
        }
        else
        {
            affichage += $"\n{k} - {t.Type} - {t.EventSurTerrain.Nom}";
        }
        k++;
    }
    Console.WriteLine($"{affichage}");
    
    int choix = DemanderAction("",potager.Terrains.Count()-1,0);

    Terrain terrainChoisi = potager.Terrains[Convert.ToInt32(choix)];

    if (terrainChoisi.EventSurTerrain == null)
    {
        Console.WriteLine("Il n'y a pas de mauvaise herbe sur ce terrain");
    }
    else if (terrainChoisi.EventSurTerrain.Nom != "🍃 De la mauvaise herbe") 
    {
        Console.WriteLine("Il n'y a pas de mauvaise herbe sur ce terrain");
    }
    else
    {
        terrainChoisi.EventSurTerrain = null;
        Console.WriteLine("Le potager a été désherbé.");
    }
    
}

void ActionArroser(Potager potager)
{
    Console.WriteLine("\nChoisissez le numéro du terrain de la plante à arroser");

    AfficherListeTerrains(potagerIrlandais);

    int choix1 = DemanderAction("", potager.Terrains.Count() - 1, 0);

    Terrain terrainChoisi = potager.Terrains[Convert.ToInt32(choix1)];

    Console.WriteLine("\nChoisissez le numéro de la plante à arroser");

    if (terrainChoisi.Plantation.Count() == 0)
    {
        Console.WriteLine("Vous n'avez aucune plante sur ce terrain, revenez lorsque vous en aurez planté.");
    }
    else
    {
        string affichage2 = ""; //affichage des plantes du potager choisi
        int i = 0;
        foreach (Plante p in terrainChoisi.Plantation)
        {
            affichage2 += $"\n{i} - {p.Nom} - {p.Hydratation} Hydratation \n";
            i++;
         }
        Console.WriteLine(affichage2);

        int choix = DemanderAction("", terrainChoisi.Plantation.Count() - 1, 0);

            
        Plante planteChoisie = potager.Terrains[Convert.ToInt32(choix1)].Plantation[choix];
        planteChoisie.Hydratation += 20;
        Console.WriteLine("La plante a été arrosée");
    }

}

void ActionTraiter(Potager potager)
{
    Console.WriteLine("\nChoissisez le numéro du terrain de la plante à traiter");

    AfficherListeTerrains(potagerIrlandais);

    int choix1 = DemanderAction("", potager.Terrains.Count() - 1, 0);

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
        foreach (Plante p in potager.Terrains[choix1].Plantation) //affichage plantes malades
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
        int choix = DemanderAction("", terrainChoisi.Plantation.Count() - 1, 0);


        Plante planteChoisie = potager.Terrains[choix1].Plantation[choix];

        if (planteChoisie.Malade==1)
        {
            planteChoisie.Malade = 0;
            planteChoisie.VitesseDeCroissance = 1;
        
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

    AfficherListeTerrains(potagerIrlandais);

    int choix1 = DemanderAction("", potager.Terrains.Count() - 1, 0);

    Terrain terrainChoisi = potager.Terrains[choix1];
    
    if (terrainChoisi.Plantation.Count == 0)
    {
        Console.WriteLine("Vous n'avez aucune plante sur ce terrain, revenez lorsque vous en aurez planté.");
    }
    else
    {
        Console.WriteLine("\nChoisissez le numéro de la plante à jeter");

        string affichage2 = "";
        int i = 0;
        foreach (Plante p in terrainChoisi.Plantation)
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
        int choix = DemanderAction("", terrainChoisi.Plantation.Count()-1, 0);
  

        Plante planteChoisie = terrainChoisi.Plantation[choix];

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

int DemanderAction(string commentaire, int valeurMax, int valeurMin)
{
    Console.WriteLine(commentaire);
    int choix = choixActionJoueur;

    do
        if (int.TryParse(Console.ReadLine()!, out int resultat))
        {
            choix = resultat;
        }
        else
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        }
    while ((choix == choixActionJoueur) || (choix < valeurMin) || (choix > valeurMax));

    return choix;

}

void AfficherListeTerrains(Potager potager) //à mettre dans potager?
{

    string affichage = ""; //affichage des terrains
    int l = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{l} - {t.Type} ";
        l++;

    }
    Console.WriteLine(affichage);
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


//Test état plante
/*Trefle plante1 = new Trefle(); //morte
Trefle plante2 = new Trefle(); //malade
Trefle plante3 = new Trefle(); //proche mort
Trefle plante4 = new Trefle(); 
Trefle plante5 = new Trefle();
Potager potager = new Potager();

TerreBrune terrain1 = new TerreBrune(4);
Console.WriteLine(terrain1.Semer(plante1));
Console.WriteLine(terrain1.Semer(plante2));
Console.WriteLine(terrain1.Semer(plante3));
Console.WriteLine(terrain1.Semer(plante4));
Console.WriteLine(terrain1.Semer(plante5));

Console.WriteLine(potager.AjouterTerrain(terrain1)); 
Console.WriteLine(plante1); 
Console.WriteLine(plante1.Compteur); 
Console.WriteLine(plante1.TerrainPlante.Type); 
Console.WriteLine(plante1.TerrainPrefere); */

