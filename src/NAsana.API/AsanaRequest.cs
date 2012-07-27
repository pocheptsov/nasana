namespace NAsana.API.v1
{
    using System.Collections.Generic;
    using RestSharp;

    internal class AsanaRequest
    {
        public static IRestRequest Get(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.GET;

            return request;
        }

        public static IRestRequest Post(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.POST;

            return request;
        }

        public static IRestRequest Delete(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.DELETE;

            return request;
        }

        public static IRestRequest Put(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.PUT;

            return request;
        }

        private static IRestRequest CreateRequest(string resource, IEnumerable<Parameter> parameters)
        {
            var request = new RestRequest();
            if (parameters != null)
            {
                request.Parameters.AddRange(parameters);
            }

            request.DateFormat = "yyyy-MM-dd";
            request.Resource = resource;
            request.RequestFormat = DataFormat.Json;
            request.RootElement = "data";

            return request;
        }
    }
}