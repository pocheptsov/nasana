namespace NAsana.API.Tests
{
    using System;
    using Mocks;
    using RestSharp;
    using v1;

    internal static class AsanaClientTestHelper
    {
        public static TAsanaClient GetAsanaClient<TAsanaClient>(string responseContent,
                                                                 Action<IRestClient> restClientUpdater = null)
            where TAsanaClient : AsanaClient
        {
            var restClient = new TestRestClient(responseContent);
            var asanaClient =
                (TAsanaClient) Activator.CreateInstance(typeof (TAsanaClient), new AsanaClient(restClient));
            return asanaClient;
        }
    }
}