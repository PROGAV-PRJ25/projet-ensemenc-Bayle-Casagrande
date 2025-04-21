// Plante plante1 = new Plante (3);
// Plante plante2 = new Plante (1);
// Plante plante3 = new Plante (2);
// Plante plante4 = new Plante (4);

// Terrain terrain1 = new Terrain(4);
// Console.WriteLine(terrain1.Semer(plante1));
// Console.WriteLine(terrain1.Semer(plante2));
// Console.WriteLine(terrain1.Semer(plante3));
// Console.WriteLine(terrain1.Semer(plante4));
// Console.WriteLine(terrain1);

// Potager potager1 = new Potager();
// Console.WriteLine(potager1.AjouterTerrain(terrain1));
// potager1.Urgence(terrain1, "Souris");





//---------programme principal structure-------------------------------

int temps = 0;
int nbTour = 5;
int modeUrgence = 0;
Potager potagerTest = new Potager();

//phase d'initialisation
    //création de 3 terrain
    // affichage des consignes
    //semis de la première plante

//tours
for (int i = 0; i<nbTour; i++)
{
    Random alea = new Random();
    modeUrgence = alea.Next(0,2);

    if (modeUrgence == 1)
    {
        //déclenchement du mode urgence
    }
    else 
    {
        //changement de météo !
        //actualisation des plantes
        //afficher état jardin
        //action joueur
        //magasin
        temps++;
    }
}




//-------------------fonctions principales de déroulement de tour--------------

void ActualiserPlantes()
{
    foreach (Terrain terrain in potagerTest.Terrains)
    {
        foreach (Plante plante in terrain.Plantation)
        {
            plante.Age++;
            plante.VerifierEtatPlante(); //vérifier les critères de terrains
            plante.TombeMalade();
            plante.Pousser(); //fait grandir la plante selon la vitesse de croissance et change son statut de récoltable
        }
    }
}


//test de croissance de plante
Plante test = new Plante(1);
Terrain terrain1 = new Terrain(4);
terrain1.Semer(test);

for (int i=0;i<15;i++)
{
    test.Pousser();
    Console.WriteLine(terrain1);
}
