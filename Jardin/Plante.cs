public abstract class Plante
{
    //---!!!!!attention mettre en protected et mettre la classe en abstract
    public string Nature {get; set;} 
    public string Nom {get; set;}
    public double VitesseDeCroissance {get; set;} // Echelle de 1 à 5 ??
    public int esperanceDeVie;
    public int EsperanceDeVie // Si cette éspérence de vie est atteinte, la plante est déclaré morte
    {
        get {return esperanceDeVie;}
        set{
                if(esperanceDeVie < 0) 
                {
                    esperanceDeVie = 0;
                }
                else
                {
                    esperanceDeVie= value;
                }
            }
    } 
    public int PrixDeVente {get; set;} 
    public int Taille {get; set;} // 1, 2, 3 ou 4
    public Terrain? TerrainPlante {get; set;} //C'est le terrain ou la plante est semé
    public int Age {get; set;} // Permet de savoir si l'éspérance de vie est dépassé ou non
    int mort; 
    public int Mort
    {
        get
        {
            return (Age > EsperanceDeVie || Compteur < 3) ? 1 : 0;
        }
    }
    // Comprit entre 0 et 5
    public int Compteur
    {
        get
        {
            int compteur = 5;
            if (TerrainPlante != null)
            {
                if (TerrainPlante.Capacite - TerrainPlante.NombreDePlante < PlaceNecessaire)
                    compteur -= 1;
                if (TerrainPlante.Type != TerrainPrefere)
                    compteur -= 1;
                if ((TerrainPlante.Humidite > BesoinHumidite * 1.5) || (TerrainPlante.Humidite < BesoinHumidite * 0.5))
                    compteur -= 1;
                if ((TerrainPlante.Temperature > BesoinTemperature * 1.5) || (TerrainPlante.Temperature < BesoinTemperature * 0.5))
                    compteur -= 1;
                if (SaisonDePlantaison != SaisonDePlantaisonPrefere)
                    compteur -= 1;
            }
            return compteur;
        }
    } //Il permet de comptabiliser combien de condition de préférence de la plante sont respecté
    public int PlaceNecessaire {get; set;} // Chaque plante a besoin d'une certaine place diponible dans le jardin pour être à l'aise
    public string TerrainPrefere {get; set;}
    public int BesoinHumidite {get; set;} // Pourcentage
    public int BesoinTemperature {get; set;} 
    public int SaisonDePlantaisonPrefere {get; set;} // 1:Printemps, 2:Ete, 3:Automne, 4:Hiver Saison ou la plante devrait etre planté
    public int SaisonDePlantaison{get; set;} // 1:Printemps, 2:Ete, 3:Automne, 4:Hiver Saison ou la plante est planté
    public Plante()
    {

    }
    public override string ToString()
    {
        if (Mort == 1)
        {
            // afficher quand meme la plante? 
            // la mettre en couleur ?
            return "La plante est morte vous devez la récolter.";
        }
        else
        {
            // Faire un cas quand la plante est proche de la mort ?
            string[] pousse = AfficherPlante(this);
            string affichage=$"- Nom : {Nom}  Age : {Age}  Taille : {Taille}\n ";
            for(int i=0; i<pousse.Length; i++)
            {
                affichage +=$"{pousse[i]}\n";
            }
            return affichage;
        }
    }
    public string[] AfficherPlante(Plante planteAfficher)
    {
        if (planteAfficher.Taille==4)
        {
            string[] pousse = new string[5];
            pousse[4]  = @" /^^\";
            pousse[3]  = "  ||";
            pousse[2]  = "  ||";
            pousse[1]  = @"\_\/_/";
            pousse[0]  = " _   _";
            return pousse;
        }
        else if (planteAfficher.Taille==3)
        {
            string[] pousse = new string[4];
            pousse[3] = @" /^^\";
            pousse[2]  = "  ||";
            pousse[1]  = "  ||";
            pousse[0]  = @"  /\";
            return pousse;
            
        }
        else if (planteAfficher.Taille==2)
        {
            string[] pousse = new string[3];
            pousse[2]  = @" /^^\";
            pousse[1]  = "  ||";
            pousse[0]  = @"  /\";
            return pousse;
        }
        else
        {
            string[] pousse = new string[2];
            pousse[1]  = @" /^^\";
            pousse[0]  = @" /\";
            return pousse;
        }
        
        
    }
    public void Pousser()
    {
        double croissance = this.Age*this.VitesseDeCroissance;
        if (this.Mort==1)
        {

        }
        else if (this.Taille==4)
        {
            Console.WriteLine("La plante est récoltable");
            //+ changer la couleur
        }
        else 
        {
            this.ChangerEtatPlante(croissance);
        }
    } 
    public virtual void ChangerEtatPlante(double croissance )//défini pour chaque plante individuellement
    {
    } 
    public void TomberMalade()
    {
        Random aleaMaladie = new Random();
        int chanceMalade = aleaMaladie.Next(1,11);
        if (chanceMalade==10)
        {
            this.VitesseDeCroissance -= 0.25;
            //+ changer la couleur
        }
    }

}
    
