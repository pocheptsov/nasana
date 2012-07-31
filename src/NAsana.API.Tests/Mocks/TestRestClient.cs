namespace NAsana.API.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using RestSharp;
    using RestSharp.Deserializers;
    using RestSharp.Extensions;

    public class TestRestClient : IRestClient
    {
        private readonly string _responseContent;

        public TestRestClient(string responseContent)
        {
            _responseContent = responseContent;
        }

        public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
        {
            throw new NotImplementedException();
        }

        public IRestResponse Execute(IRestRequest request)
        {
            var response = new RestResponse();
            response.Request = request;
            response.Content = _responseContent;

            return response;
        }

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            IRestResponse raw = Execute(request);
            return Deserialize<T>(request, raw);
        }

        private IRestResponse<T> Deserialize<T>(IRestRequest request, IRestResponse raw)
        {
            request.OnBeforeDeserialization(raw);
            IDeserializer handler = new JsonDeserializer();
            handler.RootElement = request.RootElement;
            handler.DateFormat = request.DateFormat;
            handler.Namespace = request.XmlNamespace;

            IRestResponse<T> restResponse = new RestResponse<T>();
            try
            {
                restResponse = ResponseExtensions.toAsyncResponse<T>(raw);
                restResponse.Data = handler.Deserialize<T>(raw);
            }
            catch (Exception ex)
            {
                restResponse.ResponseStatus = ResponseStatus.Error;
                restResponse.ErrorMessage = ex.Message;
                restResponse.ErrorException = ex;
            }
            return restResponse;
        }


        public Uri BuildUri(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public CookieContainer CookieContainer
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string UserAgent
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int Timeout
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool UseSynchronizationContext
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IAuthenticator Authenticator
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string BaseUrl
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<Parameter> DefaultParameters
        {
            get { throw new NotImplementedException(); }
        }

        public X509CertificateCollection ClientCertificates
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IWebProxy Proxy
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}