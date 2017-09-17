using IGDBLib;
using IGDBLib.Attributes;
using IGDBLib.Games;
using System;
using System.Collections.Generic;
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

            /*
            IGDBParams prms = new IGDBParams();
            prms.SetFields(Enum.GetValues(typeof(IGDBFields)).Cast<IGDBFields>().ToArray());
            TestGame game = IGDB.GetGameInfos<TestGame>("overwatch", prms).Result;

            Console.WriteLine(game.ID);
            Console.WriteLine(game.Name);
            Console.WriteLine("Genres: ");
            foreach (GameGenre genre in game.Genres)
                Console.WriteLine($"     {genre.ToString()}");
            Console.WriteLine("Modes: ");
            foreach (GameMode mode in game.Modes)
                Console.WriteLine($"     {mode.ToString()}");
            Console.WriteLine("Genres: ");
            foreach (GameTheme theme in game.Themes)
                Console.WriteLine($"     {theme.ToString()}");
            // Console.WriteLine(game.PEGI?.Rating);
            // Console.WriteLine(game.ScreenShots[0]?.URL); */

            GameCompany company = IGDB.GetCompanyInfos(68).Result;
            Console.WriteLine(company.ID);
            Console.WriteLine(company.Name);
            Console.WriteLine(company.Website);
            Console.WriteLine(company.Logo.URL);
            Console.Read();
        }
    }

    class TestGame
    {
        public TestGame()
        {

        }

        [IGDBValue("id")]
        public long ID { get; set; }

        [IGDBValue("name")]
        public string Name { get; set; }

        [IGDBValue("genres")]
        public List<GameGenre> Genres { get; set; } = new List<GameGenre>();
        [IGDBValue("game_modes")]
        public List<GameMode> Modes { get; set; } = new List<GameMode>();
        [IGDBValue("themes")]
        public List<GameTheme> Themes { get; set; } = new List<GameTheme>();
    }
}
