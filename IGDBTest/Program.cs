using IGDBLib;
using IGDBLib.Games;
using System;
using System.IO;
using System.Linq;

namespace IGDBTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string[] credentials = File.ReadAllLines(@"C:\IGDB\Credentials.txt");
            IGDB.USER_KEY = credentials[0];
            IGDB.USER_APICAST_URL = credentials[1];

            IGDBParams prms = new IGDBParams();
            prms.SetFields(Enum.GetValues(typeof(IGDBFields)).Cast<IGDBFields>().ToArray());
            Game game = IGDB.GetGameInfos("battlefield 1", prms).Result;

            Console.WriteLine(game.ID);
            Console.WriteLine(game.Name);
            Console.WriteLine(game.PEGI?.Rating);
            Console.WriteLine(game.ScreenShots[0]?.URL);
            Console.WriteLine("Genres: ");
            foreach (GameGenre genre in game.Genres)
                Console.WriteLine($"     {genre.ToString()}");
            Console.WriteLine("Modes: ");
            foreach (GameMode mode in game.Modes)
                Console.WriteLine($"     {mode.ToString()}");
            Console.WriteLine("Genres: ");
            foreach (GameTheme theme in game.Themes)
                Console.WriteLine($"     {theme.ToString()}");
            Console.Read();
        }
    }
}
