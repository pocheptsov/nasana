namespace NAsana.API.Tests
{
    using System;
    using Mocks;
    using v1;
    using v1.Utils;

    public class AsanaClientTests
    {
        protected bool IsIntegration;

        public TAsanaClient GetAsanaClient<TAsanaClient>(string responseContent = null) where TAsanaClient : AsanaClient
        {
            var asanaClient = responseContent == null
                                  ? Activator.CreateInstance(typeof (TAsanaClient), 
                                  new AsanaClient(new AsanaConfigManager().GetConfig()))
                                  : Activator.CreateInstance(typeof (TAsanaClient),
                                                             new AsanaClient(new TestRestClient(responseContent)));
            return (TAsanaClient) asanaClient;
        }
    }
}