Plante plante1 = new Trefle (3);
Plante plante2 = new Trefle (1);
Plante plante3 = new Trefle (2);
Plante plante4 = new Trefle (4);

Terrain terrain1 = new TerreBrune(4);
Console.WriteLine(terrain1.Semer(plante1));
Console.WriteLine(terrain1.Semer(plante2));
Console.WriteLine(terrain1.Semer(plante3));
Console.WriteLine(terrain1.Semer(plante4));
Console.WriteLine(terrain1);

Potager potager1 = new Potager();
Console.WriteLine(potager1.AjouterTerrain(terrain1));
potager1.Urgence(terrain1, "Souris");

Console.WriteLine(plante1);
Console.WriteLine(terrain1);