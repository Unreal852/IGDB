using IGDBLib.Extenders;
using IGDBLib.Rest.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IGDBLib.Rest.Request
{
    public class RestRequest
    {
        private bool m_hasFields;
        private bool m_hasExplicitBody;

        public RestRequest(HttpMethod method, string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
                throw new ArgumentException("The url passed to the HttpMethod constructor is not a valid HTTP/S URL");
            URL = uri;
            HttpMethod = method;
        }

        public Uri URL { get; private set; }

        public HttpMethod HttpMethod { get; private set; }

        public Dictionary<string, string> Headers { get; private set; } = new Dictionary<string, string>();

        public MultipartFormDataContent Body { get; private set; } = new MultipartFormDataContent();

        /// <summary>
        /// Add header 
        /// </summary>
        /// <param name="name">Header Name</param>
        /// <param name="value">Header Value</param>
        /// <returns>This</returns>
        public RestRequest AddHeader(string name, string value)
        {
            Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Add headers
        /// </summary>
        /// <param name="headers">Headers</param>
        /// <returns>This</returns>
        public RestRequest AddHeaders(Dictionary<string, string> headers)
        {
            Headers.Add(headers);
            return this;
        }

        /// <summary>
        /// Add a field
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <param name="value">Field Value</param>
        /// <returns>This</returns>
        public RestRequest AddField(string name, string value)
        {
            if (CanAddField())
            {
                Body.Add(new StringContent(value), name);
                m_hasFields = true;
            }
            return this;
        }

        /// <summary>
        /// Add a field
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="imageData">Field data ( image )</param>
        /// <returns>This</returns>
        public RestRequest AddField(string name, byte[] imageData)
        {
            if (CanAddField())
            {
                ByteArrayContent content = new ByteArrayContent(imageData);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("image.jpeg");
                Body.Add(content, name, "image.jpg");
                m_hasFields = true;
            }
            return this;
        }

        /// <summary>
        /// Add a field
        /// </summary>
        /// <param name="value">Stream value</param>
        /// <returns>This</returns>
        public RestRequest AddField(Stream value)
        {
            if (CanAddField())
            {
                Body.Add(new StreamContent(value));
                m_hasFields = true;
            }
            return this;
        }

        /// <summary>
        /// Add fields
        /// </summary>
        /// <param name="parameters">params</param>
        /// <returns>This</returns>
        public RestRequest AddFields(Dictionary<string, object> parameters)
        {
            if (CanAddField())
            {
                Body.Add(new FormUrlEncodedContent(parameters.Where(kv => kv.Value is string).Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value as string))));
                foreach (var stream in parameters.Where(kv => kv.Value is Stream).Select(kv => kv.Value))
                    Body.Add(new StreamContent(stream as Stream));
                m_hasFields = true;
            }
            return this;
        }

        /// <summary>
        /// Add body
        /// </summary>
        /// <param name="body">body</param>
        /// <returns>This</returns>
        public RestRequest AddBody(string body)
        {
            if (CanAddBody())
            {
                Body = new MultipartFormDataContent { new StringContent(body) };
                m_hasExplicitBody = true;
            }
            return this;
        }

        /// <summary>
        /// Add body
        /// </summary>
        /// <typeparam name="T">Body Type</typeparam>
        /// <param name="body">Body</param>
        /// <returns>This</returns>
        public RestRequest AddBody<T>(T body)
        {
            if (CanAddBody())
            {
                Body = new MultipartFormDataContent { new StringContent(JsonConvert.SerializeObject(body)) };
                m_hasExplicitBody = true;
            }
            return this;
        }

        /// <summary>
        /// Return the response as a string
        /// </summary>
        /// <returns>String response</returns>
        public RestResponse<string> AsString() => RestHelper.Request<string>(this);

        /// <summary>
        /// Return the response as a string asynchronously
        /// </summary>
        /// <returns>String response</returns>
        public Task<RestResponse<string>> AsStringAsync() => RestHelper.RequestAsync<string>(this);

        /// <summary>
        /// Return the response as binary
        /// </summary>
        /// <returns>Binary response</returns>
        public RestResponse<Stream> asBinary() => RestHelper.Request<Stream>(this);

        /// <summary>
        /// Return the response as binary asynchronously
        /// </summary>
        /// <returns>Binary response</returns>
        public Task<RestResponse<Stream>> AsBinaryAsync() => RestHelper.RequestAsync<Stream>(this);

        /// <summary>
        /// Return the response as json
        /// </summary>
        /// <typeparam name="T">Class to be cast</typeparam>
        /// <returns>Json response</returns>
        public RestResponse<T> AsJson<T>() => RestHelper.Request<T>(this);

        /// <summary>
        /// Return the response as json asynchronously
        /// </summary>
        /// <typeparam name="T">Class to be cast</typeparam>
        /// <returns>Json response</returns>
        public Task<RestResponse<T>> AsJsonAsync<T>() => RestHelper.RequestAsync<T>(this);

        /// <summary>
        /// Can we add a field
        /// </summary>
        /// <returns>Can add field</returns>
        private bool CanAddField()
        {
            if (HttpMethod == HttpMethod.Get)
                throw new InvalidOperationException("Can't add body to Get request.");
            if (m_hasExplicitBody)
                throw new InvalidOperationException("Can't add fields to a request with an explicit body");
            return true;
        }

        /// <summary>
        /// Can we add body
        /// </summary>
        /// <returns>Can add body</returns>
        private bool CanAddBody()
        {
            if (HttpMethod == HttpMethod.Get)
                throw new InvalidOperationException("Can't add body to Get request.");
            if (m_hasFields)
                throw new InvalidOperationException("Can't add explicit body to request with fields");
            return true;
        }
    }
}
