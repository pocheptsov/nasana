namespace NAsana.API.v1
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Exceptions;
    using Model;
    using RestSharp;
    using RestSharp.Deserializers;
    using RestSharp.Extensions;
    using Utils;

    public partial class AsanaClient
    {
        private const string BaseUrl = "https://app.asana.com/api/1.0/";

        protected readonly IRestClient Client;
        protected AsanaOptions RequestOptions;
        private AsanaRequest _asanaRequest;

        public AsanaClient(AsanaConfig config)
            : this(new RestClient(BaseUrl)
                       {
                           Authenticator =
                               new HttpBasicAuthenticator(config.ApiKey, string.Empty)
                       })
        {
        }

        protected internal AsanaClient(IRestClient restClient)
        {
            Guard.NotNull("restClient", restClient);
            Client = restClient;
            ErrorHandler =
                (ex, actionName) =>
                    {
                        if (Debugger.IsAttached && Debugger.IsLogging())
                        {
                            Debugger.Log(1, Debugger.DefaultCategory,
                                         string.Format("Error occured in a {0} method with message: {1}", actionName,
                                                       ex.Message));
                        }
                    };
        }

        public Action<Exception, string> ErrorHandler { get; set; }

        public AsanaOptions ClientOptions { get; set; }

        protected AsanaRequest AsanaRequest
        {
            get { return _asanaRequest ?? (_asanaRequest = new AsanaRequest(RequestOptions ?? ClientOptions)); }
            set { _asanaRequest = value; }
        }

        protected TModel ExecuteRequest<TModel>(IRestRequest request,
                                                HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
            where TModel : new()
        {
            // validate request
            Guard.NotNull("request", request);

            // execute the request
            var responseRaw = Client.Execute(request);

            if (responseRaw.ResponseStatus == ResponseStatus.Error)
            {
                TryThrowErrorException(request, responseRaw);
                throw new NAsanaApiException(GetErrorMessage(responseRaw), responseRaw.ErrorException);
            }

            IRestResponse<TModel> typedResponse;
            bool isDeserialized = TryDeserialize(request, responseRaw, out typedResponse);

            //error convert to typed class
            if (!isDeserialized && typedResponse.ResponseStatus == ResponseStatus.Error)
            {
                if (typedResponse.ErrorException is InvalidCastException)
                {
                    TryThrowErrorException(request, typedResponse);
                }

                throw new NAsanaApiException(GetErrorMessage(typedResponse), typedResponse.ErrorException);
            }

            // make sure the exected status code is returned
            VerifyResponse(typedResponse, expectedStatusCode);

            // return the response
            return typedResponse.Data;
        }

        private void TryThrowErrorException(IRestRequest request, IRestResponse responseRaw)
        {
            var rootElement = request.RootElement;
            request.RootElement = "";
            //try to deserialize into error response
            IRestResponse<ErrorsModel> errorResponse;
            bool isDeserialized = TryDeserialize(request, responseRaw, out errorResponse);
            request.RootElement = rootElement;

            if (isDeserialized)
            {
                var errorsModel = errorResponse.Data;
                if (errorsModel.Errors == null)
                {
                    return;
                }
                throw new NAsanaApiException(GetErrorMessage(responseRaw) + " " +
                                             string.Join(". ", errorsModel.Errors.Select(_ => _.Message)),
                                             responseRaw.ErrorException);
            }
        }

        private static string GetErrorMessage(IRestResponse response)
        {
            string errorMessage;
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    errorMessage = "Invalid request.";
                    break;
                case HttpStatusCode.Unauthorized:
                    errorMessage = "No authorization.";
                    break;
                case HttpStatusCode.Forbidden:
                    errorMessage = "Access denied.";
                    break;
                case HttpStatusCode.NotFound:
                    errorMessage = "Not found.";
                    break;
                default:
                    errorMessage = "Server error.";
                    break;
            }

            return errorMessage + " " + response.ErrorMessage;
        }

        /// <summary>
        /// Verifies whether given <paramref name="response"/> is as expected.
        /// </summary>
        /// <param name="response">The <see cref="IRestResponse"/> which to verify.</param>
        /// <param name="expectedStatusCode">The exepected status code of the request, default is <seealso cref="HttpStatusCode.OK"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        protected static void VerifyResponse(IRestResponse response,
                                             HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            // validate arguments
            Guard.NotNull("response", response);

            // check if the user was not authorized to make the request
            /*if (response.StatusCode == HttpStatusCode.Unauthorized && expectedStatusCode != HttpStatusCode.Unauthorized)
                throw new LinkedINUnauthorizedException();

            // check if the actuel status code is not the expected status code
            if (response.StatusCode != expectedStatusCode)
                throw new LinkedINHttpResponseException(expectedStatusCode, response.StatusCode, response.ErrorMessage, response.ErrorException);*/
        }

        private bool TryDeserialize<T>(IRestRequest request, IRestResponse raw, out IRestResponse<T> restResponse)
        {
            request.OnBeforeDeserialization(raw);
            IDeserializer handler = new JsonDeserializer();
            handler.RootElement = request.RootElement;
            handler.DateFormat = request.DateFormat;
            handler.Namespace = request.XmlNamespace;
            restResponse = new RestResponse<T>();
            try
            {
                restResponse = raw.toAsyncResponse<T>();
                restResponse.Data = handler.Deserialize<T>(raw);
            }
            catch (Exception ex)
            {
                restResponse.ResponseStatus = ResponseStatus.Error;
                restResponse.ErrorMessage = ex.Message;
                restResponse.ErrorException = ex;
                return false;
            }
            return true;
        }

        private Task CreateTaskImpl(Task task, long workspaceId, string[] whitelist)
        {
            var request = AsanaRequest.Post("tasks");
            if (whitelist == null || whitelist.Length == 0)
            {
                whitelist = task.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                    .Where(_ => _.Name != "Workspace")
                    .Select(_ => _.Name).ToArray();
            }
            else
            {
                //exclude Workspace original property from whitelist
                whitelist = whitelist.Where(_ => _ != "Workspace").ToArray();
            }

            request.AddObject(task, whitelist);
            //include only id of workspace
            request.AddParameter(new Parameter
                                     {
                                         Name = "workspace",
                                         Value = workspaceId,
                                     });

            return ExecuteRequest<Task>(request);
        }
    }

    public class AsanaClient<TAsanaClient> : AsanaClient where TAsanaClient : AsanaClient
    {
        public AsanaClient(AsanaConfig config)
            : base(config)
        {
        }

        protected internal AsanaClient(IRestClient restClient)
            : base(restClient)
        {
        }

        public TAsanaClient Options(AsanaOptions asanaOptions)
        {
            if (asanaOptions != null)
            {
                RequestOptions = asanaOptions;
            }
            return (TAsanaClient) (AsanaClient) this;
        }
    }
}