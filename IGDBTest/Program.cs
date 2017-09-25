using IGDBLib;
using IGDBLib.Attributes;
using IGDBLib.Games;
using IGDBLib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IGDBTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string[] credentials = File.ReadAllLines(@"C:\IGDB\Credentials.txt"); //Load Credentials from file

            IGDB.USER_KEY = credentials[0]; //Define secret key
            IGDB.USER_APICAST_URL = credentials[1]; //Define APICAST url

            Menu();

            /* Exact name match
            IGDBParams prms = new IGDBParams();
            prms.SetFields(Enum.GetValues(typeof(IGDBFields)).Cast<IGDBFields>().ToArray());
            prms.AddFilter(new IGDBFilter("name", IGDBFilterCondition.EQ, "Destiny 2"));
            string url = $"{IGDB.USER_APICAST_URL}/games/?{prms.Build()}&limit=1";
            Game game = IGDB.GetInfos<Game>(url).Result;
            Console.WriteLine(game.Summary); */

            Console.Read();
        }

        /// <summary>
        /// Main Menu
        /// </summary>
        private static void Menu()
        {
            try //Ulgy catch to avoid wrong input :D
            {
                Write("Select the test method", ConsoleColor.Cyan);
                Write("1. Game Infos", ConsoleColor.Yellow);
                Write("2. Company Infos", ConsoleColor.Yellow);

                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        GetGameInfos();
                        break;
                    case 2:
                        GetCompanyInfos();
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.Clear();
                Menu();
            }
        }

        /// <summary>
        /// Get Game Infos 
        /// </summary>
        private static void GetGameInfos()
        {
            Console.Clear();
            Write("Enter the game name:", ConsoleColor.Green);
            IGDBParams prms = new IGDBParams(); //Create Params Instance
            prms.SetFields(typeof(CGame)); //Add IGDB Fields from the object, they also can be added individualy
            //prms.SetFields(Enum.GetValues(typeof(IGDBFields)).Cast<IGDBFields>().ToArray()); //Add all IGDB Fields
            CGame game = IGDB.GetGameInfos<CGame>(Console.ReadLine(), prms).Result; //Get games infos from IGDB
            Write(" ");
            Write($"ID: {game.ID}");
            Write($"Name: {game.Name}");
            Write("Genres:");
            foreach (GameGenre genre in game.Genres)
                Write($"     {genre.ToString()}");
            Write("Modes: ");
            foreach (GameMode mode in game.Modes)
                Write($"     {mode.ToString()}");
            Write("Genres: ");
            foreach (GameTheme theme in game.Themes)
                Write($"     {theme.ToString()}");
            Write("ScreenShots: ");
            foreach (GameImage img in game.ScreenShots)
                Write($"     {img.URL}"); 
            Write($"PEGI: {game.PEGI.Rating}");
            Write(" ");
            Write("------------------------------");
            Write(" ");
            Write("Press any key to return to menu", ConsoleColor.Yellow);
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        /// <summary>
        /// Get Company Infos
        /// </summary>
        private static void GetCompanyInfos()
        {
            Console.Clear();
            Write("Enter the company ID", ConsoleColor.Green);
            GameCompany company = IGDB.GetCompanyInfos(int.Parse(Console.ReadLine())).Result; //Get company infos from IGDB
            Write(" ");
            Write($"ID: {company.ID}");
            Write($"Name: {company.Name}");
            Write($"Website: {company.Website}");
            Write(" ");
            Write("Press any key to return to menu");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        /// <summary>
        /// Log to console
        /// </summary>
        /// <param name="txt">Message</param>
        /// <param name="color">Message Color</param>
        /// <param name="pause">Pause Console</param>
        private static void Write(string txt, ConsoleColor color = ConsoleColor.White, bool pause = false)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(txt);
            Console.ResetColor();
            if (pause)
                Console.ReadKey();
        }
    }

    public class CGame
    {
        public CGame()
        {

        }

        [IGDBValue(IGDBFields.ID)]
        public long ID { get; internal set; }

        [IGDBValue(IGDBFields.NAME)]
        public string Name { get; internal set; }

        [IGDBValue(IGDBFields.PEGI)]
        public GamePEGI PEGI { get; internal set; }

        [IGDBValue(IGDBFields.SCREENSHOTS)]
        public GameImage[] ScreenShots { get; internal set; }

        [IGDBValue(IGDBFields.GENRES)]
        public GameGenre[] Genres { get; internal set; }
        [IGDBValue(IGDBFields.GAME_MODES)]
        public GameMode[] Modes { get; internal set; }
        [IGDBValue(IGDBFields.THEMES)]
        public GameTheme[] Themes { get; internal set; }
    }
}
