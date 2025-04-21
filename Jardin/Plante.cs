public class Plante 
{

    //---!!!!!attention mettre en protected et mettre la classe en abstract
    public string Nature {get; set;}
    public double VitesseDeCroissance {get; set;}
    public int EsperanceDeVie {get; set;} // Si cette éspérence de vie est atteinte, la plante est déclaré morte
    public int PrixDeVente {get; set;} 
    public int Taille {get; set;} // 1, 2, 3 ou 4
    public int Age {get; set;} // Permet de savoir si l'éspérance de vie est dépassé ou non
    public int mort; 
    public int Mort // 0 ou 1
    {
        get {return mort;}
        set{
            if(Age > EsperanceDeVie) 
            {
                mort = 1;
            }
            else
            {
                mort = 0;
            }}}
    public int PlaceNecessaire {get; set;} // Chaque plante a besoin d'une certaine place diponible dans le jardin pour être à l'aise
    public Terrain TerrainPrefere {get; set;}
    public int BesoinHumidite {get; set;}
    public int BesoinTemperature {get; set;}
    public int SaisonDePlantaison {get; set;}

    public int recoltable;

    public int Recoltable // 0 ou 1
    {
        get {return recoltable;}
        set{
            if(Taille == 4) 
            {
                recoltable = 1;
            }
            else
            {
                recoltable = 0;
            }}}


    public Plante(int taille) //enlver la taille et ajouter un terrain de plantage
    {
        Taille = taille;
        VitesseDeCroissance = 2;
        Age = 0;
        EsperanceDeVie = 1;
    }



    public override string ToString()
    {
        string[] pousse = AfficherPlante(this);
        string affichage="";
        for(int i=0; i<pousse.Length; i++)
        {
            affichage +=$"{pousse[i]}\n";
        }
        return affichage;
    }
    public string[] AfficherPlante(Plante planteAfficher)
    {
        string[] pousse = new string[5];
        if (planteAfficher.Taille==4)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = "  ||";
            pousse[1]  = @"\_\/_/";
            pousse[0]  = " _   _";
        }
        else if (planteAfficher.Taille==3)
        {
            pousse[4] = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = "  ||";
            pousse[1]  = @"  /\";
            
        }
        else if (planteAfficher.Taille==2)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = @"  /\";
        }
        else if (planteAfficher.Taille==1)
        {
            pousse[4]  = @" /^^\";
            pousse[3]  = @"  /\";
        }
        return pousse;
    }
    

    public void Pousser()
    {
        double croissance = Age*VitesseDeCroissance;

        if (this.Mort==1)
        {
            this.MortPlante();
        }
        else if (this.Recoltable==1)
        {
            Console.WriteLine("La plante est récoltable");
        }
        else 
        {
            this.ChangerEtatPlante(croissance);
        }
       

    }


    public void ChangerEtatPlante(double croissance ) //défini pour chaque plante individuellement à mettre dans les classes plus tard
    {
        if (croissance<3)
        {
            this.Taille = 1;
        }
        else if ((croissance>=3)&&(croissance<6))
        {
            this.Taille = 2;

        }
        else if ((croissance>=6)&&(croissance<9))
        {
            this.Taille = 3;
            
        }
        else if (croissance>=9)
        {
            this.Taille = 4;
            
        }
    }
    public void TombeMalade()
    {
        Random aleaMaladie = new Random();
        int chanceMalade = aleaMaladie.Next(1,11);

        if (chanceMalade==10)
        {
            this.VitesseDeCroissance -= 0.25;
        }
    }

    public void VerifierEtatPlante()
    {
        //si les 50% des conditions ne sont pas complètes
        //mort plante
    }

    public void MortPlante() // mettre la fonction dans la classe terrain ?
    {
        //enlever la plante de la liste

        Console.WriteLine("Oh non la plante est morte...");
    }
}
    
