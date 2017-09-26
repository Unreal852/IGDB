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

        /// <summary>
        /// Create rest request 
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> GetInfos<T>(string url) //TODO Handle Errors provided by IGDB
        {
            return await Task.Run(new Func<Task<T>>(async () =>
            {
                RestResponse<string> response = await Get(url);
                string responseBody = response.Body;
                List<T> objs = IGDBParser.ParseData<T>(responseBody);
                if (objs != null && objs.Count > 0)
                    return objs[0];
                return Activator.CreateInstance<T>();
            }));
        }

        /// <summary>
        /// Get games infos by name
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="gameName">Game Name</param>
        /// <param name="args">Query Args</param>
        /// <returns>Game Infos</returns>
        public static async Task<T> GetGameInfos<T>(string gameName, IGDBParams args)
        {
            string url;
            if (args != null)
                url = $"{USER_APICAST_URL}/games/?search={gameName}&{args.Build()}&limit={args.Limit.ToString()}";
            else
                url = $"{USER_APICAST_URL}/games/?search={gameName}";
            return await GetInfos<T>(url);
        }

        /// <summary>
        /// Get games infos by name
        /// </summary>
        /// <param name="gameName">Game Name</param>
        /// <param name="args">Query Args</param>
        /// <returns>Game Infos</returns>
        public static async Task<Game> GetGameInfos(string gameName, IGDBParams args)
        {
            return await GetGameInfos<Game>(gameName, args);
        }

        /// <summary>
        /// Get company infos by id
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="companyID">Company ID</param>
        /// <returns>Company Infos</returns>
        public static async Task<T> GetCompanyInfos<T>(int companyID)
        {
            string url = $"{USER_APICAST_URL}/companies/{companyID.ToString()}";
            return await GetInfos<T>(url);
        }

        /// <summary>
        /// Get company infos by id
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <returns>Company Infos</returns>
        public static async Task<GameCompany> GetCompanyInfos(int companyID)
        {
            return await GetCompanyInfos<GameCompany>(companyID);
        }
        
        /// <summary>
        /// Get engine infos by id
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="engineID">Engine ID</param>
        /// <returns>Engine Infos</returns>
        public static async Task<T> GetEngineInfos<T>(int engineID)
        {
            string url = $"{USER_APICAST_URL}/game_engines/{engineID.ToString()}";
            return await GetInfos<T>(url);
        }

        /// <summary>
        /// Get engine infos by id
        /// </summary>
        /// <param name="engineID">Engine ID</param>
        /// <returns>Engine Info</returns>
        public static async Task<GameEngine> GetEngineInfos(int engineID)
        {
            return await GetEngineInfos<GameEngine>(engineID);
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
