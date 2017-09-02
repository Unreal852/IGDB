using IGDBLib.Rest.Request;
using IGDBLib.Rest.Response;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IGDBLib.Rest
{
    public static class RestHelper
    {
        /// <summary>
        /// Create rest request
        /// </summary>
        /// <typeparam name="T">Request response type</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        public static RestResponse<T> Request<T>(RestRequest request)
        {
            Task<HttpResponseMessage> responseTask = Request(request);
            Task.WaitAll(responseTask);
            HttpResponseMessage response = responseTask.Result;
            return new RestResponse<T>(response);
        }

        /// <summary>
        /// Create aync rest request
        /// </summary>
        /// <typeparam name="T">Request response type</typeparam>
        /// <param name="request">Request</param>
        /// <returns>Response</returns>
        public static Task<RestResponse<T>> RequestAsync<T>(RestRequest request)
        {
            Task<HttpResponseMessage> responseTask = Request(request);
            return Task<RestResponse<T>>.Factory.StartNew(() =>
            {
                Task.WaitAll(responseTask);
                return new RestResponse<T>(responseTask.Result);
            });
        }

        private async static Task<HttpResponseMessage> Request(RestRequest request)
        {
            if (!request.Headers.ContainsKey("user-agent"))
                request.Headers.Add("user-agent", IGDB.USER_AGENT);
            HttpClient client = new HttpClient();
            HttpRequestMessage msg = new HttpRequestMessage(request.HttpMethod, request.URL);
            foreach (KeyValuePair<string, string> header in request.Headers)
                msg.Headers.Add(header.Key, header.Value);
            if (request.Body.Any())
                msg.Content = request.Body;
            return await client.SendAsync(msg);
        }
    }
}
