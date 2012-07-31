namespace NAsana.API.v1
{
    using System.Collections.Generic;
    using RestSharp;

    public class AsanaRequest
    {
        private readonly AsanaOptionsApplier _asanaOptionsApplier;
        private AsanaOptions _options;

        public AsanaRequest(AsanaOptions options)
        {
            if (options != null)
            {
                _options = options;
                _asanaOptionsApplier = new AsanaOptionsApplier(_options);
            }
        }

        public IRestRequest Get(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.GET;
            ApplyOptions(request, true);

            return request;
        }

        public IRestRequest Post(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.POST;
            ApplyOptions(request, false);

            return request;
        }

        public IRestRequest Delete(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.DELETE;
            ApplyOptions(request, false);

            return request;
        }

        public IRestRequest Put(string resource, params Parameter[] parameters)
        {
            var request = CreateRequest(resource, parameters);
            request.Method = Method.PUT;
            ApplyOptions(request, false);

            return request;
        }

        private void ApplyOptions(IRestRequest request, bool isGet)
        {
            if (_asanaOptionsApplier != null)
            {
                _asanaOptionsApplier.ApplyOptions(request, true);
            }
        }

        public AsanaRequest Options(AsanaOptions options)
        {
            if (options != null)
            {
                _options = options;
            }
            return this;
        }

        private IRestRequest CreateRequest(string resource, IEnumerable<Parameter> parameters)
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