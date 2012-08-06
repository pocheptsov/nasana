namespace NAsana.API.Tests
{
    using System;
    using Mocks;
    using NUnit.Framework;
    using v1;
    using v1.Utils;

    [TestFixture(false)]
    [TestFixture(true)]
    public class AsanaClientTests
    {
        private readonly bool _isIntegration;
        private readonly AsanaConfig _asanaConfig;

        public AsanaClientTests(bool isIntegration)
        {
            _isIntegration = isIntegration;
            if (isIntegration)
            {
                _asanaConfig = new AsanaConfigManager().GetConfig();
            }
        }

        public TAsanaClient GetAsanaClient<TAsanaClient>(string responseContent) where TAsanaClient : AsanaClient
        {
            var asanaClient = _isIntegration
                                  ? Activator.CreateInstance(typeof (TAsanaClient), new AsanaClient(_asanaConfig))
                                  : Activator.CreateInstance(typeof (TAsanaClient),
                                                             new AsanaClient(new TestRestClient(responseContent)));
            return (TAsanaClient) asanaClient;
        }

        [TestFixture]
        public class AsanaClientCtr
        {
        }
    }
}