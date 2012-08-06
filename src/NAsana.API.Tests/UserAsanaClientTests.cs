namespace NAsana.API.Tests
{
    using Mocks;
    using NUnit.Framework;
    using PowerAssert;
    using v1;
    using v1.Model.Utils;

    public class UserAsanaClientTests : AsanaClientTests
    {
        // ReSharper disable InconsistentNaming
        private const string short_user_response_content = "{ \"data\": " +
                                                           "{ \"email\": \"gsanchez@example.com\", \"id\": 5678, \"name\": \"Greg Sanchez\", " +
                                                           "\"workspaces\": [ " +
                                                           "{ \"id\": 1337, \"name\": \"My Favorite Workspace\" }" +
                                                           "] } }";

        private const string short_users_response_content = "{ \"data\": [ " +
                                                            "{ \"email\": \"tbizarro@example.com\", \"id\": 1234, \"name\": \"Tim Bizarro\" }, " +
                                                            "{ \"email\": \"gsanchez@example.com\", \"id\": 5678, \"name\": \"Greg Sanchez\" }] }";

        public UserAsanaClientTests(bool isIntegration)
            : base(isIntegration)
        {
        }

        [Test]
        public void create_user_asana_client()
        {
            var restClient = new TestRestClient("");
            var userAsanaClient = new AsanaClient.UserAsanaClient(new AsanaClient(restClient));

            PAssert.IsTrue(() => userAsanaClient != null);
        }

        [Test]
        public void success_get_current_user()
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(short_user_response_content);

            var user = userAsanaClient.GetCurrentUser();

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Id == 5678);
            PAssert.IsTrue(() => user.Email == "gsanchez@example.com");
            PAssert.IsTrue(() => user.Name == "Greg Sanchez");
        }

        [Test]
        public void success_get_current_user_predefined_me()
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(short_user_response_content);

            var user = userAsanaClient.GetUser(UserPredefinedId.Me);

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Id == 5678);
            PAssert.IsTrue(() => user.Email == "gsanchez@example.com");
            PAssert.IsTrue(() => user.Name == "Greg Sanchez");
        }

        [Test]
        public void success_get_user()
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(short_user_response_content);

            var user = userAsanaClient.GetUser(5678);

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Id == 5678);
            PAssert.IsTrue(() => user.Email == "gsanchez@example.com");
            PAssert.IsTrue(() => user.Name == "Greg Sanchez");
        }

        [Test]
        public void success_get_users()
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(short_users_response_content);

            var users = userAsanaClient.GetUsers();

            PAssert.IsTrue(() => users != null);
            PAssert.IsTrue(() => users.Count == 2);
            PAssert.IsTrue(() => users[0].Email == "tbizarro@example.com");
            PAssert.IsTrue(() => users[1].Name == "Greg Sanchez");
        }
    }

// ReSharper restore InconsistentNaming
}