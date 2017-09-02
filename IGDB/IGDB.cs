using IGDBLib.Games;
using IGDBLib.Rest.Request;
using IGDBLib.Rest.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IGDBLib
{
    public static class IGDB
    {
        private static readonly string GAME_COVER_URL = "https://res.cloudinary.com/igdb/image/upload/t_logo_med_2x/";

        public static string USER_AGENT = "igdb-lib/1.0";
        public static string USER_KEY = "";
        public static string USER_APICAST_URL = "";

        public static async void GetGameInfos(string gameName, int maxGames = 1, bool rawInfos = false)
        {

        }

        public static async Task<Game> GetGameInfos(string gameName, IGDBParams args, bool rawInfos = false) //TODO Handle Error
        {
            return await Task.Run(new Func<Task<Game>>(async () =>
            {
                RestResponse<string> response;
                if (args != null)
                    response = await Get($"{USER_APICAST_URL}/games/?search={gameName}&{args.Build()}&limit={args.Limit.ToString()}");
                else
                    response = await Get($"{USER_APICAST_URL}/games/?search={gameName}");
                string responseBody = response.Body;
                /*
                if (responseBody.ToLower().Contains("authentification") && responseBody.ToLower().Contains("failed"))
                    throw new AuthentificationFailedException(); */
                List<Game> games = IGDBParser.ParseGameData<Game>(responseBody);
                if (games != null && games.Count > 0)
                    return games[0];
                return null;
            }));
        }

        /// <summary>
        /// Get data from IGDB
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>Data</returns>
        private async static Task<RestResponse<string>> Get(string url)
        {
            return await new RestRequest(HttpMethod.Get, url)
                .AddHeader("user-key", USER_KEY)
                .AddHeader("Accept", "application/json")
                .AsStringAsync();
        }
    }
}
