public abstract class Terrain
{
    public int Capacite {get; set;} // Le jardin a une certaine capacité qui ne peut pas être dépasser
    public int NombreDePlante {get; set;}
    public List<Plante> Plantation {get; set;}
    public string Type {get; set;}

    public bool Event {get; set;}
    public int humidite;
    public int Humidite // En pourcentage
    {
        get {return humidite;}
        set{
                if(humidite < 0) 
                {
                    humidite = 0;
                }
                else if (humidite > 100)
                {
                    humidite = 100;
                }
                else {humidite = value;}
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

    public Terrain(int placeDisponible)
    {
        NombreDePlante = 0;
        Plantation = new List<Plante>();
        Capacite = 10;
        Event = false;
    }
    public override string ToString()
    {
        string affichage ="";
        foreach (var plante in Plantation)
        {
            affichage += plante.ToString();
        }
        return affichage;
    }
    public string Semer(Plante nouvellePlante)
    {
        if (NombreDePlante == Capacite)
        {
            return "Ce terrain n'a plus de place pour acceuillir de nouvelle plante.";
        }
        else 
        {
            NombreDePlante +=1;
            Plantation.Add(nouvellePlante);
            nouvellePlante.TerrainPlante=this;
            nouvellePlante.Age =0;
            return "La graine a été semé.";
        }
    }
    public void ChangerMeteo()
    {
        Random alea = new Random();
        int meteo = alea.Next(1,4);

        if (meteo==1) //soleil
        {
            this.Humidite -=20;
            this.Temperature += 5;
        }
        else if (meteo==2) //pluie
        {
            this.Humidite +=30;
            this.Temperature -= 5;
        }
        else if (meteo==3) //neige
        {
            this.Humidite +=25;
            this.Temperature -= 15;
        }
    }

    public void DeclencherEvent()
    {
        Random alea = new Random();
        int chance = alea.Next(1,4);

        if (chance==1)
        {
            Evenement fee = new Fee();
            //fee.BonneAction(); augmente fertilite du terrain
        }
        else if (chance==2)
        {
            Evenement insecte = new Insecte();
            //insecte.Action()
            Event = true; //au bout de 3 mois l'enlever, descend la fertilite du terrain
            //faire une fonction pour l'enlever
        }
        else
        {
            Evenement herbe = new Herbe();
            //herbe action //permanent a part si désherbage, met l'acidite à true donc aucune croissance
            Event = true;
            //faire une fonction pour l'enlever
        }
    }
}