





//---------programme principal structure-------------------------------


Console.WriteLine("fnfj");
int temps = 1;
int nbTour = 5;
int modeUrgence = 0;
Potager potagerTest = new Potager();

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
    // Random alea = new Random();
    // modeUrgence = alea.Next(0, 2);

    if (modeUrgence == 1)
    {
        //déclenchement du mode urgence
    }
    else
    {
        ChangerClimat();
        ActualiserPlantes();
        ActualiserEvent();
        AfficherEtatJardin();
        //action joueur + wiki
        //magasin
        temps++;
    }
}

Console.WriteLine("Fin de partie");


//-------------------fonctions principales de déroulement de tour--------------

void ChangerClimat()
{
    potagerTest.Saison = temps % 4;

    potagerTest.ChangerSaison();

    foreach (Terrain terrain in potagerTest.Terrains)
    {
        terrain.ChangerMeteo();
    }

}




void ActualiserPlantes()
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

void ActualiserEvent()
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

void AfficherEtatJardin()
{
    foreach (Terrain ter in potagerTest.Terrains)
    {
        Console.WriteLine("Terrain :");
        Console.WriteLine(ter);
    }
}

//test de croissance de plante
//Trefle test = new Trefle();
//TerreBrune terrain1 = new TerreBrune(4);
//terrain1.Semer(test);

//for (int i = 0; i < 15; i++)
//{
//    test.Pousser();
//    Console.WriteLine(terrain1);
//}

// test mode urgence

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
