using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IGDBLib.Rest.Response
{
    public class RestResponse<T>
    {
        public RestResponse(HttpResponseMessage response)
        {
            Code = (int)response.StatusCode;
            if (response.Content != null)
            {
                Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                Task.WaitAll(streamTask);
                Raw = streamTask.Result;

                if (typeof(T) == typeof(String))
                {
                    Task<string> stringTask = response.Content.ReadAsStringAsync();
                    Task.WaitAll(stringTask);
                    Body = (T)(object)stringTask.Result;
                }
                else if (typeof(Stream).IsAssignableFrom(typeof(T)))
                    Body = (T)(object)Raw;
                else
                {
                    Task<string> stringTask = response.Content.ReadAsStringAsync();
                    Task.WaitAll(stringTask);
                    Body = JsonConvert.DeserializeObject<T>(stringTask.Result);
                }
            }

            foreach (var header in response.Headers)
                Headers.Add(header.Key, header.Value.First());
        }

        public int Code { get; private set; }

        public Dictionary<String, String> Headers { get; private set; } = new Dictionary<string, string>();

        public T Body { get; set; }

        public Stream Raw { get; private set; }
    }
}
