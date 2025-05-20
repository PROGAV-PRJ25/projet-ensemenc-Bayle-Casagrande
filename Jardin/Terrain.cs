public abstract class Terrain
{
    public int Capacite {get; set;} // Le jardin a une certaine capacité qui ne peut pas être dépasser
    public int NombreDePlante {get; set;}
    public List<Plante> Plantation {get; set;}
    public string Type {get; set;}

    public List<Evenement> EventSurTerrain {get; set;}
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

    public double fertilite = 1; //influence croissance du terrain entre 0 et 1,5

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
    public bool acidite = false; //les plantes ne poussent plus
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

    public int temperature;
    public int Temperature 
    {
        get {return temperature;}
        set{
                if(temperature < 0) 
                {
                    temperature = 0;
                }
                else
                {
                    temperature= value;
                }
            }
    }

    public bool Event {get; set;}
    public Potager PotagerTerrain {get; set;}
    public string Meteo {get; set;}





    public Terrain(int placeDisponible)
    {
        NombreDePlante = 0;
        Plantation = new List<Plante>();
        Capacite = 10;
        Event = false;
        EventSurTerrain = new List<Evenement>();
    }
    public override string ToString()
    {
        
        string affichage = "";
        foreach (Evenement e in EventSurTerrain)
        {
            affichage += e.ToString();
            Console.WriteLine(e.ToString());
        }
        if (Plantation.Count ==0)
        {
            return "Vous n'avez pas encore de plante dans ce terrain. \n";
        }
        affichage +="Dans ce terrain vous avez :  \n";
        foreach (var plante in Plantation)
        {
            affichage += plante.ToString();
        }
        return affichage;
    }
    public virtual string Semer(Plante nouvellePlante, int temps)
    {
        if (NombreDePlante == Capacite)
        {
            return "\nCe terrain n'a plus de place pour accueillir de nouvelles plantes.\n";
        }
        else 
        {
            string affichage ="\nLa graine a été semée dans ce terrain.";
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
            return affichage;
        }
    }
    public void ChangerMeteo() //totem bool if false déclenchement aléa
    {
        Random alea = new Random();
        int nbAlea = alea.Next(1,4);

        if (nbAlea==1) //soleil
        {
            this.Meteo = "Soleil";
            this.Temperature += 10;
        }
        else if (nbAlea==2) //pluie
        {
            this.Meteo = "Pluie";
            this.Humidite +=30;
        }
        else if (nbAlea==3) //neige
        {
            this.Meteo = "Neige";
            this.Temperature -= 15;
        }
        else if (nbAlea==4)
        {
            this.Meteo = "Vent";
            this.Humidite -= 20;
        }
    }

    public void DeclencherEvent()
    {
        Random alea = new Random();
        int chance = alea.Next(1,11);
        string affichage ="";
        if (chance==1)
        {
            Evenement fee = new Fee();
            Event = true;
            this.EventSurTerrain.Add(fee);
            affichage = fee.ToString();
            affichage += $"{Type} !!";
        }
        else if (chance==2)
        {
            Evenement insecte = new Insecte();
            //insecte.Action()
            Event = true;
            this.EventSurTerrain.Add(insecte);
            affichage = insecte.ToString();
            affichage += $"{Type} !!";
            //au bout de 3 mois l'enlever, descend la fertilite du terrain
            //faire une fonction pour l'enlever
        }
        else if (chance==3)
        {
            Evenement herbe = new Herbe();
            //herbe action //permanent a part si désherbage, met l'acidite à true donc aucune croissance
            Event = true;
            this.EventSurTerrain.Add(herbe);
            affichage = herbe.ToString();
            affichage += $"{Type} !!";
            //faire une fonction pour l'enlever
        }
    }

public string CalculerSaisonPlantaison(int temps)
{
    int mois = temps%12;
    if (mois<4)
    {
        return "Printemps";
    }
    else if (mois<8)
    {
        return "Ete";
    }
    else if (mois<12)
    {
        return "Automne";
    }
    else 
    {
        return "Hiver";
    }
    
}
}