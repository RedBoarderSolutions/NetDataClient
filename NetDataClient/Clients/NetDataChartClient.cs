using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RedBoarder.NetDataClient.Base;
using RedBoarder.NetDataClient.Dtos;

namespace RedBoarder.NetDataClient.Clients
{
    public class NetDataChartClient : BaseClient
    {
        public NetDataChartClient(IHttpClientFactory httpClientfactory, ILogger<NetDataChartClient> logger) : base(httpClientfactory, logger, "NetDataClient")
        {
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)},
                IgnoreNullValues = true
            };
        } 

        public async Task<NetDataResult?> GetChartData(NetDataChart chart, int after, int before, string group,
            int points, CancellationToken cancellationToken = default)
        {
            const string endpoint =
                "data?chart={chart}&after={after}&before={before}&points={points}&group={group}&gtime=0&format=json&options=seconds,jsonwrap";

            var urlBuilder = new StringBuilder();
            urlBuilder.Append(endpoint); 
            urlBuilder.Replace("{chart}", chart.Value);
            urlBuilder.Replace("{after}", ConvertToString(after));
            urlBuilder.Replace("{before}", ConvertToString(before));
            urlBuilder.Replace("{points}", ConvertToString(points));
            urlBuilder.Replace("{group}", ConvertToString(group));

            var requestendpoint = urlBuilder.ToString();
            using var request =  new HttpRequestMessage(HttpMethod.Get, requestendpoint);
          
            try
            {
                using var response = await GetClient()
                    .SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);

                var status = ((int)response.StatusCode).ToString();
                if (status == "200")
                {

                    var objectResponse = await ReadHttpResponseAsync<NetDataResult>(response);
                    return objectResponse.Object;
                }
                else
                {
                    _logger.LogError($"Unbale to Get NetData Status with response Status {status}");
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to Get NetData Status");
                return null;
            }
        }

    }
}