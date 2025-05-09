

using System.Runtime.CompilerServices;

// Jeu();



//---------programme principal structure-------------------------------

// void Jeu()
// {
    Console.WriteLine("Bienvenu dans le jeu du potager Irlandais !");
    int temps = 1;
    int nbTour = 5;
    int modeUrgence = 0;
    Potager potagerTest = new Potager();
    Magasin magasin = new Magasin(10);

    Trefle plante1 = new Trefle(); //morte
    Trefle plante2 = new Trefle(); //malade
    Trefle plante3 = new Trefle(); //proche mort
    Trefle plante4 = new Trefle(); 
    Trefle plante5 = new Trefle();

    TerreBrune terrain1 = new TerreBrune(4);
    Console.WriteLine(terrain1.Semer(plante1));
    Console.WriteLine(terrain1.Semer(plante2));
    Console.WriteLine(terrain1.Semer(plante3));
    Console.WriteLine(terrain1.Semer(plante4));
    Console.WriteLine(terrain1.Semer(plante5));

    Console.WriteLine(potagerTest.AjouterTerrain(terrain1));

    //phase d'initialisation
    //création de 3 terrain
    // affichage des consignes
    //semis de la première plante

    //tours
    for (int i = 0; i < nbTour; i++)
    {
        System.Threading.Thread.Sleep(500);
        Console.WriteLine($"#### {i} Mois ###");
        ChangerClimat(potagerTest,temps);
        ActualiserPlantes(potagerTest);
        ActualiserEvent(potagerTest);
        Console.WriteLine(potagerTest); //affichage du potager
        ActionJoueur(2);//action joueur + wiki
        //magasin
        temps++;

    }

    Console.WriteLine("Fin de partie");
// }

//-------------------fonctions principales de déroulement de tour--------------

void ChangerClimat(Potager potagerTest, int temps)
{
    potagerTest.Saison = temps % 4;

    potagerTest.ChangerSaison();

    foreach (Terrain terrain in potagerTest.Terrains)
    {
        terrain.ChangerMeteo();
    }

}




void ActualiserPlantes(Potager potagerTest)
{
    foreach (Terrain terrain in potagerTest.Terrains)
    {
        foreach (Plante plante in terrain.Plantation)
        {
            plante.Age++;
            plante.TomberMalade();
            plante.Pousser(); //fait grandir la plante selon la vitesse de croissance et change son statut de récoltable
        }
    }
}

void ActualiserEvent(Potager potagerTest)
{
    foreach (Terrain terrain in potagerTest.Terrains)
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

void ActionJoueur(int nbAction)
{
    for (int i = 0;i<nbAction;i++)
    {
        Console.WriteLine("Que souhaitez-vous faire ?");
        Console.WriteLine("1-Semer\n2-Récolter\n3-Désherber\n4-Arroser\n5-Traiter\n6-Jeter\n7-Wiki");
        int reponse = Convert.ToInt32(Console.ReadLine()!); //mettre un vérif de cas

        switch (reponse)
        {
            case 1 :
            ActionSemer();
            break;

            case 2 :
            ActionRecolter();
            break;

            case 3 :
            ActionDesherber();
            break;

            case 4 :
            ActionArroser();
            break;

            case 5 :
            ActionTraiter();
            break;

            case 6 :
            ActionJeter();
            break;

        }
    }
}

void ActionSemer() //à tester + vérif !!!
{
    Console.WriteLine("Choisissez une graine à semer dans votre inventaire");

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

            affichage += $"{i}- {p.Nom}";
            i++;
        
        }
        Console.WriteLine($"{affichage}");

        int choix = Convert.ToInt32(Console.ReadLine()!); 
        
        while ((choix<0)||(choix>magasin.GrainesAchetes.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choix = Convert.ToInt32(Console.ReadLine()!);
        }

        Console.WriteLine("Où souhaitez-vous la planter ?");
        string listTer = "";
        int j = 0;
        foreach(Terrain t in potagerTest.Terrains)
        {
            affichage += $"{j}- {t.Type}";
            j++;
        }
        Console.WriteLine($"{listTer}");

        int choixPlanter = Convert.ToInt32(Console.ReadLine()!); 

        while ((choixPlanter<0)||(choix>potagerTest.Terrains.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choixPlanter = Convert.ToInt32(Console.ReadLine()!);
        }

        Terrain terrainChoisi = potagerTest.Terrains[choixPlanter];
        Plante graineChoisie = magasin.GrainesAchetes[choix];
        terrainChoisi.Semer(graineChoisie);
        magasin.GrainesAchetes.Remove(graineChoisie);
    }
    
}

void ActionRecolter()
{
    Console.WriteLine("Choisissez une plante mûre à récolter");

    if (potagerTest.PlantesRecoltables.Count()==0)
    {
        Console.WriteLine("Aucune plante n'est mûre dans le potager.");
    }
    else
    {
        string affichage ="";
        int i = 0;

        foreach (Plante p in potagerTest.PlantesRecoltables)
        {
            affichage += $"{i}- {p.Nom} - {p.TerrainPlante.Type}";
            i++;
        }
        Console.WriteLine($"{affichage}");

        int choix = Convert.ToInt32(Console.ReadLine()!);
        while ((choix<0)||(choix>potagerTest.PlantesRecoltables.Count()))
        {
            Console.WriteLine("Saisie incorrecte, veuillez recommencer");
            choix = Convert.ToInt32(Console.ReadLine()!);
        }

        Plante planteChoisie = potagerTest.PlantesRecoltables[choix];
        magasin.PlantesRecoltes.Add(planteChoisie);
        potagerTest.PlantesRecoltables.Remove(planteChoisie); 
        planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);
        Console.WriteLine("La plante a été récoltée");
    }  
}

void ActionDesherber()
{
    Console.WriteLine("Choisissez le terrain à désherber");

    string affichage = "";
    int k = 0;
    foreach (Terrain t in potagerTest.Terrains)
    {
        affichage += $"{k}- {t.Type} - {t.EventSurTerrain[0].Nom}";
        k++;
    }
    Console.WriteLine($"{affichage}");

    int choix = Convert.ToInt32(Console.ReadLine()!); 

    while ((choix<0)||(choix>potagerTest.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix = Convert.ToInt32(Console.ReadLine()!);
    }
    
    Terrain terrainChoisi = potagerTest.Terrains[choix];
    if (terrainChoisi.EventSurTerrain[0].Nom!="De la mauvaise herbe")
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

void ActionArroser()
{
    Console.WriteLine("Choisissez une plante à arroser");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potagerTest.Terrains)
    {
        affichage += $"{l}- {t.Type}";

    }
    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potagerTest.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }

    Terrain terrainChoisi = potagerTest.Terrains[choix1];

    int i = 0;
    foreach (Plante p in terrainChoisi.Plantation)
    {
        affichage += $"{i}- {p.Nom} - {p.Hydratation}";
        i++;
    }
    int choix = Convert.ToInt32(Console.ReadLine()!); //faire une vérif aussi

    while ((choix<0)||(choix>terrainChoisi.Plantation.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix = Convert.ToInt32(Console.ReadLine()!);
    }

    Plante planteChoisie = potagerTest.Terrains[choix1].Plantation[choix];

    planteChoisie.Hydratation +=15;

    Console.WriteLine("La plante a été arrosée");

}

void ActionTraiter()
{
    Console.WriteLine("Choissisez la plante à traiter");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potagerTest.Terrains)
    {
        affichage += $"{l}- {t.Type}";

    }
    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potagerTest.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }

    Terrain terrainChoisi = potagerTest.Terrains[choix1];

    int i = 0;
    foreach (Plante p in potagerTest.Terrains[choix1].Plantation)
    {
        if (p.Malade==0)
        {
            affichage += $"{i}- {p.Nom}";
            i++;
        }
        else
        {
            affichage += $"{i}- {p.Nom} - Malade";
            i++;
        }
        
    }
    int choix = Convert.ToInt32(Console.ReadLine()!);

    while ((choix<0)||(choix>terrainChoisi.Plantation.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix = Convert.ToInt32(Console.ReadLine()!);
    }

    Plante planteChoisie = potagerTest.Terrains[choix1].Plantation[choix];

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

void ActionJeter()
{
    Console.WriteLine("Choissisez la plante à jeter");

    string affichage = "";
    int l = 0;
    foreach (Terrain t in potagerTest.Terrains)
    {
        affichage += $"{l}- {t.Type}";

    }
    int choix1 = Convert.ToInt32(Console.ReadLine()!);

    while ((choix1<0)||(choix1>potagerTest.Terrains.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix1 = Convert.ToInt32(Console.ReadLine()!);
    }
    Terrain terrainChoisi = potagerTest.Terrains[choix1];

    int i = 0;
    foreach (Plante p in potagerTest.Terrains[choix1].Plantation)
    {
        if (p.Mort==1)
        {
            affichage += $"{i}- {p.Nom} - Morte";
            i++;
        }
        else
        {
            affichage += $"{i}- {p.Nom}";
            i++;
        }
    }
    int choix = Convert.ToInt32(Console.ReadLine()!);

    while ((choix<0)||(choix>terrainChoisi.Plantation.Count()))
    {
        Console.WriteLine("Saisie incorrecte, veuillez recommencer");
        choix = Convert.ToInt32(Console.ReadLine()!);
    }

    Plante planteChoisie = potagerTest.Terrains[choix1].Plantation[choix];

    if (planteChoisie.Mort==1)
    {
        planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);
    
        Console.WriteLine("La plante morte a été jetée");
    }
    else 
    {
        Console.WriteLine("Cette plante est encore en vie, la jeter quand même? (oui/non)");

        string reponse = Console.ReadLine()!;

        while ((reponse!="oui")||(reponse!="non"))
        {
            Console.WriteLine("Réponse incorrecte, veuillez réessayez");
            reponse = Console.ReadLine()!;
        }

        if (reponse=="oui")
        {
            planteChoisie.TerrainPlante.Plantation.Remove(planteChoisie);
    
            Console.WriteLine("La plante a été jetée");
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

