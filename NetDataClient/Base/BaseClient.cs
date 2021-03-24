using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RedBoarder.NetDataClient.Base
{
    public abstract class BaseClient
    {
        private readonly IHttpClientFactory _httpClientfactory;
        protected readonly ILogger<BaseClient> _logger;
        private readonly string _clientName;
        protected JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        public bool ReadResponseAsString { get; set; } = false;


        protected HttpClient GetClient()
        {
            return _httpClientfactory.CreateClient(_clientName??""); 
        }
        

        protected BaseClient(IHttpClientFactory httpClientfactory,ILogger<BaseClient> logger,string clientName)
        {
            _httpClientfactory = httpClientfactory;
            _logger = logger;
            _clientName = clientName;
        }


        /// <summary>
        ///  Process Response and Put headers in a Dictionary 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="headers"></param>
        protected void ProcessResponseHeader(HttpResponseMessage response,
            out Dictionary<string, IEnumerable<string>> headers)
        {
            var responseHeader = response.Headers;
            headers = responseHeader.ToDictionary(h => h.Key, h => h.Value);
            foreach (var (key, value) in response.Content.Headers)
                headers[key] = value;
        }


        protected virtual async Task<HttpResponseResult<T>> ReadHttpResponseAsync<T>(HttpResponseMessage response)
        {
            if (response?.Content == null)
            {
                return new HttpResponseResult<T>(default, string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                try
                {
                    var typedBody = JsonSerializer.Deserialize<T>(responseText, _jsonSerializerOptions);
                    return new HttpResponseResult<T>(typedBody, responseText);
                }
                catch (JsonException exception)
                {
                    throw new Exception((int) response.StatusCode + "  " + exception);
                }
            }
            else
            {
                try
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    var typedBody = await JsonSerializer.DeserializeAsync<T>(responseStream, _jsonSerializerOptions);
                    return new HttpResponseResult<T>(typedBody, string.Empty);
                }
                catch (JsonException exception)
                {
                    throw new Exception((int) response.StatusCode + "  " + exception);
                }
            }
        }


        /// <summary>
        ///  Convert Enum , bool and Array to Strings 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>

        protected string? ConvertToString(object value, CultureInfo cultureInfo = default)
        {
            switch (value)
            {
                case Enum _:
                {
                    var name = Enum.GetName(value.GetType(), value);
                    if (name != null)
                    {
                        var field = value.GetType().GetTypeInfo().GetDeclaredField(name);
                        if (field != null)
                        {
                            if (field.GetCustomAttribute(typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                            {
                                return attribute.Value ?? name;
                            }
                        }
                    }

                    break;
                }
                case bool _:
                    return Convert.ToString(value, cultureInfo)?.ToLowerInvariant();
                case byte[] bytes:
                    return Convert.ToBase64String(bytes);
                default:
                {
                    if (value.GetType().IsArray)
                    {
                        var array = ((Array) value).OfType<object>();
                        return string.Join(",", array.Select(o => ConvertToString(o, cultureInfo)));
                    }

                    break;
                }
            }

            return Convert.ToString(value, cultureInfo);
        }
    }
}