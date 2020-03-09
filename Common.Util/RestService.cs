using Newtonsoft.Json;
using RestSharp;
using System;

namespace Common.Util
{
    public static class RestService
    {
        public static string Send(string url, string token, object data, Method method)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);

            request.AddHeader("Authorization", $"Bearer {0}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(data), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            var content = response.Content;

            return content;
        }
    }
}
