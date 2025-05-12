using System.Security.Cryptography.X509Certificates;

public abstract class Plante
{
    //---!!!!!attention mettre en protected et mettre la classe en abstract
    public string Nature { get; set; }
    public int Numero {get; set;} //Utiliser lors de la vente de la plante
    public string Nom { get; set; }
    public double VitesseDeCroissance { get; set; } // Echelle de 1 à 5 ??
    public int esperanceDeVie;
    public int EsperanceDeVie // Si cette éspérence de vie est atteinte, la plante est déclaré morte
    {
        get { return esperanceDeVie; }
        set
        {
            if (esperanceDeVie < 0)
            {
                esperanceDeVie = 0;
            }
            else
            {
                esperanceDeVie = value;
            }
        }
    }
    public int PrixDeVente { get; set; } // Une fois mûre
    public int PrixAchatGraine {get; set;} //Achat de la graine
    public int Taille { get; set; } // 1, 2, 3 ou 4
    public Terrain TerrainPlante { get; set; } //C'est le terrain ou la plante est semé
    public int Age { get; set; } // Permet de savoir si l'éspérance de vie est dépassé ou non

    public int mort;
    public int Mort
    {
        get
        {
            //return (Age > EsperanceDeVie || Compteur < 3) ? 1 : 0;
            return mort;
        }
        set //pour test à enlever apres
        {
            mort = value;
            Console.WriteLine($"{mort}");
        }
    }

    // Comprit entre 0 et 5
    public int Compteur{get; set;} //Il permet de comptabiliser combien de condition de préférence de la plante sont respecté
    public int PlaceNecessaire { get; set; } // Chaque plante a besoin d'une certaine place diponible dans le jardin pour être à l'aise
    public string TerrainPrefere { get; set; }
    public int BesoinHumidite { get; set; } // Pourcentage
    public int BesoinTemperature { get; set; }
    public int SaisonDePlantaisonPrefere { get; set; } // 1:Printemps, 2:Ete, 3:Automne, 4:Hiver Saison ou la plante devrait etre planté
    public int SaisonDePlantaison { get; set; } // 1:Printemps, 2:Ete, 3:Automne, 4:Hiver Saison ou la plante est planté

    public int Hydratation {get; set;} // à refaire mieux !
    public int Malade { get; set; }
    public Plante()
    {
        Hydratation = 80;

    }
    
    public override string ToString()
    {
        Compteur = MettreAJourCompteur();
        if (Mort == 1)
        {
            // afficher quand meme la plante? 
            // la mettre en couleur ?
            // Console.ResetColor();
            return "La plante est morte vous devez la jeter.";
        }
        else
        {
            // Faire un cas quand la plante est proche de la mort ?
            string[] pousse = AfficherPlante(this);
            string affichage=$"- Numéro : {Numero} Nom : {Nom}  Age : {Age}  Taille : {Taille}\n ";
            if ((Taille == 4) && (Mort == 0))
            {
                // Console.ResetColor();
                affichage += $"Cette plante est mûr et prêt à être récolté\n";
            }
            if ((Age == EsperanceDeVie - 1) || (Compteur == 3)) // Cas quand la plante est proche de la mort 
            {
                // Console.ResetColor();
                affichage += $"Attention cette plante est proche de la mort !\n";
            }
            if (Malade == 1)
            {
                affichage += $"Attention cette plante est malade...\n";
            }
            for(int i=0; i<pousse.Length; i++)
            {
                affichage +=$"{pousse[i]}\n";
            }
            return affichage;
        }
    }
    public string[] AfficherPlante(Plante planteAfficher)
    {
        if (planteAfficher.Taille == 4)
        {
            string[] pousse = new string[5];
            pousse[4] = @" /^^\";
            pousse[3] =  "  ||";
            pousse[2] =  "  ||";
            pousse[1] = @"\_\/_/";
            pousse[0] =  "_   _";
            return pousse;
        }
        else if (planteAfficher.Taille == 3)
        {
            string[] pousse = new string[4];
            pousse[3] = @" /^^\";
            pousse[2] =  "  ||";
            pousse[1] =  "  ||";
            pousse[0] = @"  /\";
            return pousse;

        }
        else if (planteAfficher.Taille == 2)
        {
            string[] pousse = new string[3];
            pousse[2] = @" /^^\";
            pousse[1] =  "  ||";
            pousse[0] = @"  /\";
            return pousse;
        }
        else
        {
            string[] pousse = new string[2];
            pousse[1] = @" /^^\";
            pousse[0] = @"  /\";
            return pousse;
        }
    }
    public void Pousser()
    {
        if (Taille==4)
        {
            TerrainPlante.PotagerTerrain.PlantesRecoltables.Add(this); //ajout de la plante à la liste des plantes récoltables du potager
        }

        if (TerrainPlante.Acidite!=true)
        {
            Hydratation -=12;
            double croissance = this.Age * this.VitesseDeCroissance*TerrainPlante.Fertilite;//ajouter acidité du terrain
            this.ChangerTaillePlante(croissance);
        }

        if (Hydratation <=0)
        {
            Mort = 1;
        }
    
        

    }
    public abstract void ChangerTaillePlante(double croissance);//défini pour chaque plante individuellement

    public void TomberMalade()
    {
        Random aleaMaladie = new Random();
        int chanceMalade = aleaMaladie.Next(1, 11);
        if (chanceMalade == 10)
        {
            this.VitesseDeCroissance -= 0.25;
            Malade = 1;
        }
    }

    public int MettreAJourCompteur()
    {
        int compteur = 5;
        if (TerrainPlante != null)
        {
            if (TerrainPlante.Capacite - TerrainPlante.NombreDePlante < PlaceNecessaire)
                {compteur -= 1;}
            if (TerrainPlante.Type != TerrainPrefere)
                {compteur -= 1;}
            if ((TerrainPlante.Humidite > BesoinHumidite * 1.2) || (TerrainPlante.Humidite < BesoinHumidite * 0.2))
                {compteur -= 1;}
            if ((TerrainPlante.Temperature > BesoinTemperature * 1.2) || (TerrainPlante.Temperature < BesoinTemperature * 0.2))
                {compteur -= 1;}
            if (SaisonDePlantaison != SaisonDePlantaisonPrefere)
                {compteur -= 1;}
        }
        return compteur;
    }
    // public void ChangerCouleur()
    // {
    //     if (Malade == 1)
    //     {
    //         Console.ForegroundColor = ConsoleColor.Green;
    //     }
    //     else if (Mort == 1)
    //     {
    //         Console.ForegroundColor = ConsoleColor.DarkBlue;
    //     }
    //     else if ((Taille == 4) && (Mort == 0)) //plante mure
    //     {
    //         Console.ForegroundColor = ConsoleColor.DarkYellow;
    //     }
    //     else if ((Age == EsperanceDeVie - 1) || (Compteur == 3))
    //     {
    //         Console.ForegroundColor = ConsoleColor.DarkRed;
    //     }
    // }
}

