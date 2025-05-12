

using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
//-------------variables de base-----------
/*int temps = 0;
int nbTour = 10;
int modeUrgence = 0;
int argentJoueur = 10;

//-------- création des objets--------
Potager potagerIrlandais = new Potager();
Magasin magasin = new Magasin(argentJoueur);
TerreBrune terrainTerreBrune = new TerreBrune(4);
Tourbiere terrainTourbiere = new Tourbiere(6);
Gleys terrainGleys = new Gleys(10);
potagerIrlandais.AjouterTerrain(terrainGleys);
potagerIrlandais.AjouterTerrain(terrainTerreBrune);
potagerIrlandais.AjouterTerrain(terrainTourbiere);

//---------programme principal structure-------------------------------

//phase d'introduction
PresenterIntroduction(nbTour);

//tours
 while (temps < nbTour)
 {
    temps +=1;
    ActiverModeUrgence(potagerIrlandais);
    Console.Clear();
    Console.WriteLine($"\n\n%%%%%%%%% Mois {temps} %%%%%%%%%\n");
    ChangerClimat(potagerIrlandais,temps);
    ActualiserPlantes(potagerIrlandais);
    ActualiserEvent(potagerIrlandais);
    System.Threading.Thread.Sleep(1000);
    Console.WriteLine(potagerIrlandais);
    System.Threading.Thread.Sleep(1000);
    FaireActionJoueur(6, magasin, potagerIrlandais);//action joueur + wiki
    System.Threading.Thread.Sleep(1000);

}


Console.WriteLine("\n\nFin de partie - Vous avez récolté tant de plantes, vous avez gagné tant d'argent,  ");*/


//-------------------fonctions principales de déroulement de tour--------------

void PresenterIntroduction(int nbTour)
{
    Console.Clear();
    Console.WriteLine("Bienvenu dans le jeu du potager Irlandais !\nRègles : \n Vous avez trois terrains dans votre potager Irlandais. Il faudra s'occuper de vos plantes ! Mais attention aux désagréments...\nCombien de mois souhaitez-vous jouer ?\n");
    // Console.WriteLine("Règles : \n");
    // Console.WriteLine("Vous avez trois terrains dans votre potager Irlandais. Il faudra s'occuper de vos plantes ! Mais attention aux désagréments...\n");
    nbTour = Convert.ToInt32(Console.ReadLine()!);
    
    Console.WriteLine($"\nVous avez {nbTour} mois pour utiliser au maximum votre potager Irlandais. Bonne chance !\n");
    System.Threading.Thread.Sleep(6000);
    Console.Clear();
     
}

void ActiverModeUrgence(Potager potager)
{
    Random alea = new Random();
    int modeUrgence;
    modeUrgence = alea.Next(0, 10);
    int terrainToucher=alea.Next(0,3);
    if (modeUrgence == 1)
    {
        int typeInt = alea.Next(0,2);
        string type ="";
        if (typeInt==0){type = "Souris";}
        else {type = "Tempête";}
        potager.Urgence(potager.Terrains[terrainToucher], type);
    }
     System.Threading.Thread.Sleep(1000);

}

void ChangerClimat(Potager potagerTest, int temps)
{
    potagerTest.Saison = temps % 4;

    potagerTest.ChangerSaison();

    foreach (Terrain terrain in potagerTest.Terrains)
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


void RentrerMagasin(Magasin magasin)
{
    Console.WriteLine(magasin);
    string actionJoueurMagasin = "";
    while (actionJoueurMagasin != "rien")
    {
        Console.WriteLine("\nVoulez vous 'acheter', 'vendre' ou ne 'rien' faire ?\n");
        actionJoueurMagasin =Console.ReadLine()!;
        if (actionJoueurMagasin == "acheter")
        {
            Console.WriteLine("\nQuelle plante ?\n");
            string planteChoisie = Convert.ToString(Console.ReadLine()!);
            Console.WriteLine(magasin.Acheter(planteChoisie));
        }
        if (actionJoueurMagasin == "vendre")
        {
            Console.WriteLine("\nQuelle plante ? Donnez son numéro\n");
            int numeroChoisie = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(magasin.Vendre(numeroChoisie));
        }
    }
}


void FaireActionJoueur(int nbAction, Magasin magasin, Potager potager)
{
    string reponse = "";
    //for (int i = 0;i<nbAction;i++)
    while (reponse !="9")
    {
        Console.WriteLine("Que souhaitez-vous faire ?");
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
            ActionSemer(magasin, potager);
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


void ActionSemer(Magasin magasin, Potager potager)
{
    Console.WriteLine("\nChoisissez une graine à semer dans votre inventaire");

    if (magasin.GrainesAchetes.Count()==0)
    {
        Console.WriteLine("Aucune graine disponible dans l'inventaire");
    }
    else
    {
        string affichage ="";
        int i = 0;

        foreach (Plante p in magasin.GrainesAchetes)
        {

            affichage += $"{i} - {p.Nom}\n";
            i++;
        
        }
        Console.WriteLine($"{affichage}");

        int choix = Convert.ToInt32(Console.ReadLine()!); 
        
        while ((choix<0)||(choix>magasin.GrainesAchetes.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choix = Convert.ToInt32(Console.ReadLine()!);
        }

        Console.WriteLine("\nOù souhaitez-vous la planter ?");
        string listTer = "";
        int j = 0;
        foreach(Terrain t in potager.Terrains)
        {
            listTer += $"\n{j} - {t.Type}";
            j++;
        }
        Console.WriteLine(listTer);

        int choixPlanter = Convert.ToInt32(Console.ReadLine()!); 

        while ((choixPlanter<0)||(choix>potager.Terrains.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choixPlanter = Convert.ToInt32(Console.ReadLine()!);
        }

        Terrain terrainChoisi = potager.Terrains[choixPlanter];
        Plante graineChoisie = magasin.GrainesAchetes[choix];
        Console.WriteLine(terrainChoisi.Semer(graineChoisie));
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

void ActionDesherber(Potager potager) // Ne fonctionne pas dans le cas ou aucun event s'est réaliser (si la liste event est vide)
{
    Console.WriteLine("\nChoisissez le terrain à désherber");

    string affichage = "";
    int k = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{k} - {t.Type} - {t.EventSurTerrain[0].Nom}";
        k++;
    }
    Console.WriteLine($"{affichage}");

    int choix = Convert.ToInt32(Console.ReadLine()!); 

    while ((choix<0)||(choix>potager.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix = Convert.ToInt32(Console.ReadLine()!);
    }
    
    Terrain terrainChoisi = potager.Terrains[choix];
    if (terrainChoisi.EventSurTerrain[0].Nom!="De la mauvaise herbe") //liste event peut etre vide ! cela crer un probleme je pense
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
    Console.WriteLine("\nChoisissez le terrain de la plante à arroser");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{l} - {t.Type}\n";

    }

    Console.WriteLine(affichage);

    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potager.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }

    Terrain terrainChoisi = potager.Terrains[choix1];

    Console.WriteLine("\nChoisissez la plante à arroser");

    string affichage2 ="";
    int i = 0;
    foreach (Plante p in terrainChoisi.Plantation)
    {
        affichage2 += $"\n{i} - {p.Nom} - {p.Hydratation} Hydratation \n";
        i++;
    }
    Console.WriteLine(affichage2);

    int choix = Convert.ToInt32(Console.ReadLine()!); //faire une vérif aussi

    while ((choix<0)||(choix>terrainChoisi.Plantation.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix = Convert.ToInt32(Console.ReadLine()!);
    }

    Plante planteChoisie = potager.Terrains[choix1].Plantation[choix];

    planteChoisie.Hydratation +=15;

    Console.WriteLine("La plante a été arrosée");

}

void ActionTraiter(Potager potager)
{
    Console.WriteLine("\nChoissisez le terrain de la plante à traiter");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{l} - {t.Type}";

    }
    Console.WriteLine(affichage);
    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potager.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }

    Terrain terrainChoisi = potager.Terrains[choix1];

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

void ActionJeter(Potager potager)
{
    Console.WriteLine("\nChoissisez le terrain de la plante à jeter");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potager.Terrains)
    {
        affichage += $"\n{l} - {t.Type}\n";

    }
    Console.WriteLine(affichage);

    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potager.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }
    Terrain terrainChoisi = potager.Terrains[choix1];
    
    Console.WriteLine("\nChoisissez la plante à jeter");
    string affichage2="";
    int i = 0;
    foreach (Plante p in potager.Terrains[choix1].Plantation)
    {
        if (p.Mort==1)
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

    while ((choix<0)||(choix>terrainChoisi.Plantation.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix = Convert.ToInt32(Console.ReadLine()!);
    }

    Plante planteChoisie = potager.Terrains[choix1].Plantation[choix];

    if (planteChoisie.Mort==1)
    {
        planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);
    
        Console.WriteLine("La plante morte a été jetée");
    }
    else 
    {
        Console.WriteLine("Cette plante est encore en vie, la jeter quand même? (oui/non)");

        string rep = Console.ReadLine()!;

        while ((rep!="oui")&&(rep!="non"))
        {
            Console.WriteLine("Réponse incorrecte, veuillez réessayez");
            rep = Console.ReadLine()!;
        }

        if (rep=="oui")
        {
            planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);
    
            Console.WriteLine("La plante a été jetée");
        }
        else if (rep=="non")
        {
            Console.WriteLine("La plante n'a pas été jetée");
        }
    }
}



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
Trefle plante1 = new Trefle(); //morte
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
Console.WriteLine(plante1.TerrainPrefere); 

