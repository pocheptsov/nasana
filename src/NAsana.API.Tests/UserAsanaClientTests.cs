namespace NAsana.API.Tests
{
    using Mocks;
    using NUnit.Framework;
    using PowerAssert;
    using v1;
    using v1.Model.Utils;

    public class UserAsanaClientTests : AsanaClientTests
    {
        private const string short_current_user_response_content = "{\"data\":" +
                                                           "{\"id\":730528258877,\"name\":\"Slava Pocheptsov\",\"email\":\"pocheptsov.test@gmail.com\"," +
                                                           "\"workspaces\":[" +
                                                           "{\"id\":730528258883,\"name\":\"Tvene Workspace\"}," +
                                                           "{\"id\":1384990514055,\"name\":\"NAsana\"}," +
                                                           "{\"id\":498346170860,\"name\":\"Personal Projects\"}]}}";

        private const string short_users_response_content = "{ \"data\": [ " +
                                                            "{ \"email\": \"tbizarro@example.com\", \"id\": 1234, \"name\": \"Tim Bizarro\" }, " +
                                                            "{ \"email\": \"gsanchez@example.com\", \"id\": 5678, \"name\": \"Greg Sanchez\" }] }";

        [Test]
        public void create_user_asana_client()
        {
            var restClient = new TestRestClient("");
            var userAsanaClient = new AsanaClient.UserAsanaClient(new AsanaClient(restClient));

            PAssert.IsTrue(() => userAsanaClient != null);
        }

        [TestCase(short_current_user_response_content)]
        [TestCase(null)]
        public void success_get_current_user(string responseContent)
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(responseContent);

            var user = userAsanaClient.GetCurrentUser();

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Id == 730528258877);
            PAssert.IsTrue(() => user.Email.StartsWith("pocheptsov"));
            PAssert.IsTrue(() => user.Email.EndsWith("@gmail.com"));
            PAssert.IsTrue(() => user.Name == "Slava Pocheptsov");
        }

        [Test]
        public void success_get_current_user_predefined_me()
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(short_current_user_response_content);

            var user = userAsanaClient.GetUser(UserPredefinedId.Me);

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Id == 730528258877);
            PAssert.IsTrue(() => user.Email.StartsWith("pocheptsov"));
            PAssert.IsTrue(() => user.Email.EndsWith("@gmail.com"));
            PAssert.IsTrue(() => user.Name == "Slava Pocheptsov");
        }

        [Test]
        public void success_get_user()
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(short_current_user_response_content);

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
}