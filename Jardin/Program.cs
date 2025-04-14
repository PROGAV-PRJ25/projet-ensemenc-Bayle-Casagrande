
// Console.Write(@"/\  /\"+"\n"+@"\_\/_/"+"\n"+ @"  || "+ "\n"+"  || "+"\n"+"  || "+"\n"+@" /^^\");
// Console.WriteLine(@" _   _"+"\n"+@"\_\/_/"+"\n"+ @"  || "+ "\n"+"  || "+"\n"+"  || "+"\n"+@" /^^\");
// Console.WriteLine(@"  /\ "+ "\n"+"  || "+"\n"+"  || "+"\n"+@" /^^\");
// Console.WriteLine(@"  /\ "+"\n"+"  || "+"\n"+@" /^^\");

// Console.WriteLine(@"  /\ "+"\n"+@" /^^\");



List<Plante> potager = new List<Plante>();
string[,] tabPotag = new string[5,4]; //nb de plantes à la place de 2
Plante a = new Plante(1);
potager.Add(a);
Plante b = new Plante(3);
potager.Add(b);
Plante c = new Plante(4);
potager.Add(c);
Plante d = new Plante(2);
potager.Add(d);




//-----on plante les plantes dans le potager
int i=0;
foreach (Plante plante in potager)
{
  AfficherPlante(plante,i);
  i++;
}


//----- affichage du potager
for (int k=0;k<5;k++) //chaque colonne
{
  for (int j=0;j<potager.Count;j++) //chaque ligne
  {
    Console.Write($"{tabPotag[k,j]}\t");
  }
  Console.WriteLine("");
}


//on adapte la forme de la plante dans le potager en fonction de sa taille et de sa localisation
void AfficherPlante(Plante plante, int col)
{
  if (plante.Taille==3)
  {
    tabPotag[4,col] = @" /^^\";
    tabPotag[3,col] = "  ||";
    tabPotag[2,col] = "  ||";
    tabPotag[1,col] = @"  /\";
    
  }
  else if (plante.Taille==4)
  {
    tabPotag[4,col] = @" /^^\";
    tabPotag[3,col] = "  ||";
    tabPotag[2,col] = "  ||";
    tabPotag[1,col] = @"\_\/_/";
    tabPotag[0,col] = " _   _";
  }
  else if (plante.Taille==2)
  {
    tabPotag[4,col] = @" /^^\";
    tabPotag[3,col] = "  ||";
    tabPotag[2,col] = @"  /\";
  }
  else if (plante.Taille==1)
  {
    tabPotag[4,col] = @" /^^\";
    tabPotag[3,col] = @"  /\";
  }
}
// //ajoute chaque plante avec un tab



 

