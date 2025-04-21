public class Potager
{
    public int Saison {get; set;}
    public List<Terrain> Terrains {get; set;}
    public Potager()
    {
        Terrains = new List<Terrain>();
    }
    public override string ToString()
    {
        return "";
    }
    public string AjouterTerrain(Terrain nouveauTerrain)
    {
        Terrains.Add(nouveauTerrain);
        return "Le terrain a été ajouté au potager.";
    }
    public void Urgence(Terrain terrain, string type)
    {
        int taille = terrain.Capacite;
        string[,] grille = CreerPotagerDynamique(taille, terrain.Plantation.Count);

        if (type == "Souris")
        {
            Random random = new Random();
            int positionLigne = random.Next(0,taille/2);
            int positionColonne = random.Next(0,taille/2);
            int sensHorizontale =1;
            int sensVerticale=1;
            for(int i=0; i<30; i++)
            {
                grille[positionLigne,positionColonne]=null;
                DeplacerSouris(ref positionLigne, ref positionColonne, ref sensHorizontale, ref sensVerticale, taille);
                grille[positionLigne,positionColonne]="🐁";
                Console.Clear();
                Console.WriteLine(AfficherPotagerDynamique(taille, grille));
                System.Threading.Thread.Sleep(200);
            }
        }
    }

    public string AfficherPotagerDynamique(int taille, string[,] grille)
    {
        string retour = "";
        for (int i = 0; i < taille/2; i++)
        {
            for (int j= 0; j < taille/2; j++)
            {   
                if (grille[j,i] == null)
                {
                    retour += " . ";
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
        // 2 plantes par ligne poitionner aléatoirement
        string [,] grille = new string[taille/2, taille/2];
        int ligne = 0;
        int colonne = 0;
        for(int i=0; i<nbPlante; i+=2)
        {
            int nombreAleatoire = random.Next(0,taille/2);
            colonne = nombreAleatoire;
            grille [colonne, ligne] = "🌿";
            while (colonne == nombreAleatoire)
            {
                nombreAleatoire = random.Next(0,taille/2);
            }
            colonne = nombreAleatoire;
            grille [colonne, ligne] = "🌿";
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



    public void ChgmtSaison()
    {
        
        if (this.Saison==1)
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite = 50;
                terrain.Temperature = 16;
            }
        }
        else if (this.Saison==2)
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite = 30;
                terrain.Temperature = 21;
            }
        }
        else if (this.Saison==3)
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite = 70;
                terrain.Temperature = 12;
            }
        }
        else
        {
            foreach (Terrain terrain in this.Terrains)
            {
                terrain.Humidite = 80;
                terrain.Temperature = 5;
            }
        }
    }
}