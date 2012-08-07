namespace NAsana.API.Tests
{
    using Mocks;
    using NUnit.Framework;
    using PowerAssert;
    using v1;
    using v1.Model.Utils;
    using System.Linq;

    public class UserAsanaClientTests : AsanaClientTests
    {
        private const string short_current_user_response_content = "{\"data\":" +
                                                           "{\"id\":730528258877,\"name\":\"Slava Pocheptsov\",\"email\":\"pocheptsov.test@gmail.com\"," +
                                                           "\"workspaces\":[" +
                                                           "{\"id\":730528258883,\"name\":\"Tvene Workspace\"}," +
                                                           "{\"id\":1384990514055,\"name\":\"NAsana\"}," +
                                                           "{\"id\":498346170860,\"name\":\"Personal Projects\"}]}}";

        private const string short_users_response_content = "{\"data\":[" +
                                                            "{\"id\":734307518840,\"name\":\"arthur\"}," +
                                                            "{\"id\":208917561095,\"name\":\"Vadym\"}," +
                                                            "{\"id\":730528258877,\"name\":\"Slava Pocheptsov\"}," +
                                                            "{\"id\":739553725184,\"name\":\"rjenya\"}," +
                                                            "{\"id\":739967000162,\"name\":\"Mikhail\"}," +
                                                            "{\"id\":740026183174,\"name\":\"Sergey\"}]}";

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

        [TestCase(short_current_user_response_content)]
        [TestCase(null)]
        public void success_get_current_user_predefined_me(string responseContent)
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(responseContent);

            var user = userAsanaClient.GetUser(UserPredefinedId.Me);

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Id == 730528258877);
            PAssert.IsTrue(() => user.Email.StartsWith("pocheptsov"));
            PAssert.IsTrue(() => user.Email.EndsWith("@gmail.com"));
            PAssert.IsTrue(() => user.Name == "Slava Pocheptsov");
        }

        [TestCase(short_current_user_response_content)]
        [TestCase(null)]
        public void success_get_user(string responseContent)
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(responseContent);

            var user = userAsanaClient.GetUser(730528258877);

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Id == 730528258877);
            PAssert.IsTrue(() => user.Email.StartsWith("pocheptsov"));
            PAssert.IsTrue(() => user.Email.EndsWith("@gmail.com"));
            PAssert.IsTrue(() => user.Name == "Slava Pocheptsov");
        }

        [TestCase(short_users_response_content)]
        [TestCase(null)]
        public void success_get_users(string responseContent)
        {
            var userAsanaClient =
                GetAsanaClient<AsanaClient.UserAsanaClient>(responseContent);

            var users = userAsanaClient.GetUsers();

            PAssert.IsTrue(() => users != null);
            PAssert.IsTrue(() => users.Count > 0);

            var user = users.FirstOrDefault(_ => _.Id == 730528258877);

            PAssert.IsTrue(() => user != null);
            PAssert.IsTrue(() => user.Email == null);
            PAssert.IsTrue(() => user.Name == "Slava Pocheptsov");
        }
    }
}