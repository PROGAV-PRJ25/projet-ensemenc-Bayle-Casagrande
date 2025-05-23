using System.Security.Cryptography.X509Certificates;

public abstract class Plante
{
    //------attributs et accesseurs------

    //caractéristiques plantes

    protected string? Nature { get; set; } //type de la plante
    public string? Nom { get; set; }
    public Terrain? TerrainPlante { get; set; } //C'est le terrain où la plante est semée
    public string? SaisonDePlantaison { get; set; } //saison durant laquelle la plante est plantée
    public int PrixDeVente { get; set; } // Quand la plante est mûre, elle peut être vendue
    public int PrixAchatGraine {get; set;} //Achat de la graine


    //croissance et pousse de la plante

    public double VitesseDeCroissance { get; set; } // coef qui determine la taille de la plante
    public int Taille { get; set; } // 4 formes de la plantes, selon leur corissance 1, 2, 3 ou 4
    //une plante mûre est une plante de taille 4

    //conditions de vie et de mort de la plante

    public int EsperanceDeVie { get; set; } //durée de vie en mois
    public int Age { get; set; } // Âge de la plante, permet de savoir si l'espérance de vie est dépassée ou non
    public int Mort //la plante est déclarée morte si son âge dépasse son espérance de vie ou si le compteur est inférieur à 3 (càd trop de conditions néfastes pour la plante)
    {
        get
        {
            return (Age > EsperanceDeVie || Compteur < 3 ) ? 1 : 0; //ou si l'hydratation tombe à 0
        }
        set 
        {; }
    }
    public int Malade { get; set; }
    protected int Compteur { get; set; } //Il permet de comptabiliser combien de condition de préférence de la plante sont respectés

    //besoins de la plante
    
    public int PlaceNecessaire { get; set; } // Chaque plante a besoin d'une certaine place diponible dans le jardin pour être à l'aise
    public string TerrainPrefere { get; set; }
    public int BesoinHumidite { get; set; } 
    public int BesoinTemperature { get; set; }
    public string SaisonDePlantaisonPrefere { get; set; }


    //------constructeur----

    public Plante()
    {

        // Valeur par défault
        SaisonDePlantaisonPrefere = "Printemps";
        TerrainPrefere = "Terre Brune";
    }

    //----------méthodes---------


    //-------méthodes liées au conditons des plantes-------
    
    public void TomberMalade()
    {
        Random aleaMaladie = new Random();
        int chanceMalade = aleaMaladie.Next(0, 5); //1 chance sur 5 pour que la plante tombe malade

        if (chanceMalade == 1) //1 chance sur 10 que la plante tombe effectivement malade
        {
            this.VitesseDeCroissance = 0.75; //diminue sa vitesse de croissance de 25%
            Malade = 1; //actualisation de sa condition
        }
    }

    public int MettreAJourCompteur() //permet de compter les conditions satisfaisantes ou non pour la plante
    {
        int compteur = 5; //les conditions de la plante sont évaluées sur 5 points

        if (TerrainPlante != null) //à partir du moment où la plante est plantée
        {
            if (TerrainPlante.Capacite - TerrainPlante.NombreDePlante < PlaceNecessaire) //si la place restante sur le terrain est inférieure à la place nécessaire de la plante
            { compteur -= 1; }
            if (TerrainPlante.Type != TerrainPrefere) //si la plante n'est pas sur son terrain préféré
            { compteur -= 1; }
            if ((TerrainPlante.Humidite > BesoinHumidite * 1.5) || (TerrainPlante.Humidite < BesoinHumidite * 0.5)) //Les plantes acceptent une marge de 40% à partir de leur besoin en humidité
            { compteur -= 1; }
            if ((TerrainPlante.Temperature > BesoinTemperature * 1.5) || (TerrainPlante.Temperature < BesoinTemperature * 0.5)) //Les plantes acceptent une marge de 40% à partir de leur besoin en température
            { compteur -= 1; }
            if (SaisonDePlantaison != SaisonDePlantaisonPrefere) //si la plante n'est pas plantée lors de sa saison de prédilection
            { compteur -= 1; }
        }
        return compteur;
    }


    //-------------méthodes liées à la croissance, à la pousse et à la forme des plantes-----------------

    public abstract void ChangerTaillePlante(double croissance);//définie pour chaque plante individuellement, chaque plante à des seuils de croissance 

    public void Pousser() //fonction que les plantes se développent selon leur âge et leur vitesse de croissance chaque mois
    {
        if (TerrainPlante!.Acidite!=true) //si le terrain où se trouve la plante est acide (en raison d'un évènement), les plantes ne poussent plus temporairement
        { 
            //si le terrain n'est pas acide
            double croissance = this.Age * this.VitesseDeCroissance*TerrainPlante.Fertilite;//la plante croît selon son age, sa vitesse de croissance et la fertilité du terrain
            this.ChangerTaillePlante(croissance); //selon le nb obtenu pour la croissance, la plante atteint un certain seuil de croissance et sa taille est modifiée
        }
       
        if (Taille==4) //si après l'actualisation de la taille de la plante, la taille est de 4
        {
            TerrainPlante.PotagerTerrain!.PlantesRecoltables.Add(this); //ajout de la plante à la liste des plantes récoltables du potager
        }
    
    }

    //------affichage--------
    
    public string AfficherProblemePlante() //permet de savoir quelles conditions de la plante ne sont pas satisfaites
    {
        string affichage = "";

        if (TerrainPlante != null)
        {
            if (TerrainPlante.Capacite - TerrainPlante.NombreDePlante < PlaceNecessaire)
            { affichage += "🔔 Cette plante se sent très serrée.\n"; }

            if (TerrainPlante.Humidite < BesoinHumidite * 0.5)
            { affichage += "🔔 L'humidité est trop basse pour cette plante. Vous pouvez l'arroser.\n"; }

            if (TerrainPlante.Humidite > BesoinHumidite * 1.5)
            { affichage += "🔔 L'humidité est trop élevée pour cette plante.\n"; }

            //Les autres problèmes tels que la temperature, la saison de plantaison ou le terrain qui ne seraient pas bon ne sont pas affiché s
            //Car le joueur ne peut rien y faire
        }

        return affichage;
    }

    public string[] AfficherPlante(Plante planteAfficher)
    {
        //affichage graphique de la forme de la plante en fonction de sa taille
        //il y a 4 tailles qui évoluent au cours du temps et de la vitesse de croissance

        if (planteAfficher.Taille == 4) //taille où la plante est récoltable
        {
            string[] pousse = new string[5];
            pousse[4] = @" /^^\";
            pousse[3] = "  ||";
            pousse[2] = "  ||";
            pousse[1] = @"\_\/_/";
            pousse[0] = "_   _";
            return pousse;
        }
        else if (planteAfficher.Taille == 3)
        {
            string[] pousse = new string[4];
            pousse[3] = @" /^^\";
            pousse[2] = "  ||";
            pousse[1] = "  ||";
            pousse[0] = @"  /\";
            return pousse;

        }
        else if (planteAfficher.Taille == 2)
        {
            string[] pousse = new string[3];
            pousse[2] = @" /^^\";
            pousse[1] = "  ||";
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
    
    public override string ToString()
    {
        Compteur = MettreAJourCompteur(); //le compteur est mis à jour à chaque fois que la plante est affichée

        if (Mort == 1)
        {
            return $"- La plante {Nom} est morte vous devez la jeter.\n";
        }
        else
        {
            string[] pousse = AfficherPlante(this); //affichage de la plante en graphique, récupération de la pousse

            string affichage = $"- Nom : {Nom} | Age : {Age} | Taille : {Taille}\n "; //résumé de la plante et de ses conditions

            affichage += AfficherProblemePlante(); //affichage des problème de la plante

            //---------alertes majeures---------

            if ((Taille == 4) && (Mort == 0))
            {
                affichage += $"🚨 Cette plante est mûre et prête à être récoltée\n";
            }
            if ((Age == EsperanceDeVie - 1) || (Compteur == 3)) // Càd quand la plante est proche de la mort 
            {
                affichage += $"🚨 Attention cette plante est proche de la mort !\n";
            }
            if (Malade == 1)
            {
                affichage += $"🚨Attention cette plante est malade...\n";
            }
            for (int i = 0; i < pousse.Length; i++) //affichage du graphique de la plante
            {
                affichage += $"{pousse[i]}\n";
            }
            return affichage; //retourne l'affichage d'une plante
        }
    }
}

