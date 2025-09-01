using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<string, string> engToSpa = new(StringComparer.OrdinalIgnoreCase);
    static Dictionary<string, string> spaToEng = new(StringComparer.OrdinalIgnoreCase);

    static void Main()
    {
        InicializarDiccionario();
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("==== MENÚ ====\n1. Traducir frase\n2. Agregar palabra\n0. Salir");
            Console.Write("Opción: ");
            
            if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = -1;

            if (opcion == 1) TraducirFrase();
            else if (opcion == 2) AgregarPalabra();
            else if (opcion != 0) Console.WriteLine("Opción no válida.");

            if (opcion != 0) { Console.WriteLine("\nPresione una tecla..."); Console.ReadKey(); }
        } while (opcion != 0);
    }

    static void InicializarDiccionario()
    {
        string[,] baseWords = {
            {"time","tiempo"},{"person","persona"},{"year","año"},{"way","camino"},
            {"day","día"},{"thing","cosa"},{"man","hombre"},{"world","mundo"},
            {"life","vida"},{"hand","mano"},{"part","parte"},{"child","niño"},
            {"eye","ojo"},{"woman","mujer"},{"place","lugar"},{"work","trabajo"},
            {"week","semana"},{"case","caso"},{"point","punto"},{"company","empresa"}
        };
        for (int i = 0; i < baseWords.GetLength(0); i++)
            AgregarPareja(baseWords[i, 0], baseWords[i, 1]);
    }

    static void AgregarPareja(string eng, string spa)
    {
        engToSpa[eng] = spa;
        spaToEng[spa] = eng;
    }

    static void TraducirFrase()
    {
        Console.Write("Frase: ");
        string frase = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(frase)) return;

        bool esIngles = frase.Split().Count(w => engToSpa.ContainsKey(Limpiar(w))) >= 
                        frase.Split().Count(w => spaToEng.ContainsKey(Limpiar(w)));

        var traduccion = frase.Split().Select(w =>
        {
            string limpio = Limpiar(w);
            string traducido = esIngles
                ? engToSpa.GetValueOrDefault(limpio, limpio)
                : spaToEng.GetValueOrDefault(limpio, limpio);
            return traducido + ExtraerPuntuacion(w);
        });

        Console.WriteLine("Traducción: " + string.Join(" ", traduccion));
    }

    static string Limpiar(string w) => new string(w.Where(c => !char.IsPunctuation(c)).ToArray()).ToLower();
    static string ExtraerPuntuacion(string w) => new string(w.Where(char.IsPunctuation).ToArray());

    static void AgregarPalabra()
    {
        Console.Write("Inglés: "); string eng = Console.ReadLine().Trim().ToLower();
        Console.Write("Español: "); string spa = Console.ReadLine().Trim().ToLower();
        if (eng != "" && spa != "")
        {
            AgregarPareja(eng, spa);
            Console.WriteLine("Palabra agregada con éxito.");
        }
    }
}
