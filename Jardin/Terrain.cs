public abstract class Terrain
{
    //-------------accesseurs et attributs------------
    
    //----caractéristiques du terrain-----
    public string Type { get; set; }
    public int Capacite { get; set; } // Le jardin a une certaine capacité qui ne peut pas être dépassée
    public int NombreDePlante {get; set;} //Nb de plantes plantées dans le terrain
    public List<Plante> Plantation {get; set;} //Liste de toutes les plantes présentes sur le terrain
    public Potager PotagerTerrain {get; set;} //Potager auquel il appartient
    public List<Evenement> EventSurTerrain { get; set; } //liste des event sur le terrain

    //-----conditions sur le terrain--------
    protected int humidite = 50;
    public int Humidite // En pourcentage
    {
        get { return humidite;}
        set{
                humidite = value;
                if(humidite < 0) 
                {
                    humidite = 0;
                }
                else if (humidite > 100)
                {
                    humidite = 100;
                }
            }
    }

    protected double fertilite = 1; //influence la croissance des plantes du terrain entre 0 et 1,5
    public double Fertilite
    {
        get
        {
            return fertilite; 
        }
        set
        {
            if(fertilite < 0.5) 
                {
                    fertilite = 0.5;
                }
                else if (fertilite > 1.5)
                {
                    fertilite=1.5;
                }
                else {fertilite = value;}
        }
    }

    protected bool acidite = false; //Si True, les plantes ne poussent plus
    public bool Acidite
    {
        get
        { 
            return acidite;
        }
        set
        {
            acidite = value;
        }
    }

    protected int temperature;
    public int Temperature //comprise entre 0 et 35
    {
        get {return temperature;}
        set{
                if (temperature < 0)
                {
                    temperature = 0;
                }
                else if (temperature > 35)
                {
                    temperature = 35;  
                }
                else
                {
                    temperature = value;
                }
            }
    }

<<<<<<< HEAD
    public bool Event {get; set;}
    public Potager PotagerTerrain {get; set;}
    public string? Meteo {get; set;}
=======
    public bool Event {get; set;} //Si True, un event a lieu sur le terrain
    public string Meteo {get; set;}
>>>>>>> 80a01195c1ee960b24e8478f0805e1db085df58b


    //--------------Constructeur-----------
    public Terrain()
    {
        NombreDePlante = 0;
        Plantation = new List<Plante>();
        Capacite = 10;
        Event = false;
        EventSurTerrain = new List<Evenement>();
        ChangerMeteo(); //La valeur Meteo est ainsi initialiser
    }
    
    //--------------affichage terrain----------------
    public override string ToString() //pour afficher les plantes et les events du terrain
    {
        string affichage = "";

        foreach (Evenement e in EventSurTerrain) //affichage des events
        {
            affichage += e.ToString();
        }

        if (Plantation.Count == 0) //si aucune plante dans terrain
        {
            return "Vous n'avez pas encore de plante dans ce terrain. \n";
        }
        affichage = "Dans ce terrain vous avez :  \n";

        foreach (var plante in Plantation) //sinon affichage de chaque plante avec leur méthode
        {
            affichage += plante.ToString();
        }
        return affichage;
    }
    
    //-------------méthodes utiles au terrain---------------
    public virtual string Semer(Plante nouvellePlante, int temps)
    {
        if (NombreDePlante == Capacite)
        {
            return "\nCe terrain n'a plus de place pour accueillir de nouvelles plantes.\n";
        }
        else
        {
<<<<<<< HEAD
            string affichage ="\nLa graine a été semée dans ce terrain.\n";
            NombreDePlante +=1;
            Plantation.Add(nouvellePlante);
            nouvellePlante.TerrainPlante=this;
            nouvellePlante.Age =0;
            nouvellePlante.SaisonDePlantaison=CalculerSaisonPlantaison(temps);
            if (nouvellePlante.TerrainPlante.Type != nouvellePlante.TerrainPrefere)
                {affichage += "Mais cette graine n'a pas été semée dans son terrain préféré...\n";}
            if (nouvellePlante.TerrainPlante.Capacite - nouvellePlante.TerrainPlante.NombreDePlante < nouvellePlante.PlaceNecessaire)
                {affichage += "Cette graine se sent très serrée sur ce terrain...\n";}
            if (nouvellePlante.SaisonDePlantaison != nouvellePlante.SaisonDePlantaisonPrefere)
                {affichage += "Cette graine n'a pas été plantée à la bonne saison...\n";}
=======
            string affichage = "\nLa graine a été semée dans ce terrain.";
            NombreDePlante += 1;
            Plantation.Add(nouvellePlante); //ajout de la new plante à la liste du terrain

            //initialisation des attributs pour la plante qui dépendent du terrain et du temps
            nouvellePlante.TerrainPlante = this; 
            nouvellePlante.Age = 0;
            nouvellePlante.SaisonDePlantaison = CalculerSaisonPlantaison(temps);

            //affichage des avertissements si les conditions de la plante ne sont pas respectées
            if (nouvellePlante.TerrainPlante.Type != nouvellePlante.TerrainPrefere)
            { affichage += "\nMais cette graine n'a pas été semée dans son terrain préféré...\n"; }
            if (nouvellePlante.TerrainPlante.Capacite - nouvellePlante.TerrainPlante.NombreDePlante < nouvellePlante.PlaceNecessaire)
            { affichage += "\nCette graine se sent très serrée sur ce terrain...\n"; }
            if (nouvellePlante.SaisonDePlantaison != nouvellePlante.SaisonDePlantaisonPrefere)
            { affichage += "\nCette graine n'a pas été plantée à la bonne saison...\n"; }

>>>>>>> 80a01195c1ee960b24e8478f0805e1db085df58b
            return affichage;
        }
    }
    
    public void ChangerMeteo() //change de manière aléatoire la météo sur le terrain
    {
        Random alea = new Random();
        int nbAlea = alea.Next(1, 4);

        if (nbAlea == 1) //soleil
        {
            this.Meteo = "Soleil";
            this.Temperature += 10;
        }
        else if (nbAlea == 2) //pluie
        {
            this.Meteo = "Pluie";
            this.Humidite += 30;
        }
        else if (nbAlea == 3) //neige
        {
            this.Meteo = "Neige";
            this.Temperature -= 15;
        }
        else if (nbAlea == 4)
        {
            this.Meteo = "Vent";
            this.Humidite -= 20;
        }
    }

    public void DeclencherEvent() //déclenche de manière aléatoire un event sur le terrain si il n'y a pas d'event en cours (càd Event=true)
    {
        Random alea = new Random();
        int chance = alea.Next(1,11); //environ 1 chance sur 3 de tomber sur un des 3 types d'event
        string affichage ="";
        
        if (chance == 1)
        {
            Evenement fee = new Fee();
            Event = true;
            this.EventSurTerrain.Add(fee);
            affichage = fee.ToString();
            affichage += $"{Type} !!";
        }
        else if (chance == 2)
        {
            Evenement insecte = new Insecte();
            Event = true;
            this.EventSurTerrain.Add(insecte);
            affichage = insecte.ToString();
            affichage += $"{Type} !!";
        }
        else if (chance == 3)
        {
            Evenement herbe = new Herbe();
            Event = true;
            this.EventSurTerrain.Add(herbe);
            affichage = herbe.ToString();
            affichage += $"{Type} !!";
        }
    }

    public string CalculerSaisonPlantaison(int temps) //calcul la saison et renvoie sa valeur
    {
        int mois = temps % 12;

        if (mois < 4)
        {
            return "Printemps";
        }
        else if (mois < 8)
        {
            return "Ete";
        }
        else if (mois < 12)
        {
            return "Automne";
        }
        else
        {
            return "Hiver";
        }

    }
}