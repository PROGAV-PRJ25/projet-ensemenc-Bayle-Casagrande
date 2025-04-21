public class Terrain
{
    public int Capacite {get; set;} // Le jardin a une certaine capacité qui ne peut pas être dépasser
    public int NombreDePlante {get; set;}
    public List<Plante> Plantation {get; set;}

    public int humidite;
    public int Humidite // 0 ou 1
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
            }
    }

    public int temperature;


    public int Temperature {get; set;}

    public Terrain(int placeDisponible)
    {
        NombreDePlante = 0;
        Plantation = new List<Plante>();
        Capacite = 10;
    }
    public override string ToString()
    {
        string affichage ="";
        for (int i=0; i<Plantation.Count(); i++)
        {
            affichage += Plantation[i].ToString();
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
}