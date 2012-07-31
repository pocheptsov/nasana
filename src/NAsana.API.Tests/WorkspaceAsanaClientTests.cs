namespace NAsana.API.Tests
{
    using Mocks;
    using NUnit.Framework;
    using v1;
    using v1.Model.Utils;

    [TestFixture]
    public class WorkspaceAsanaClientTests
    {
// ReSharper disable InconsistentNaming
        private const string short_workspace_response_content = "{\"data\":[{\"id\":1346674263886,\"name\":\"test task\"}," +
                                                                "{\"id\":1346674263887,\"name\":\"some not useful task\"}," +
                                                                "{\"id\":1346674263888,\"name\":\"moreover\"}]}";
// ReSharper restore InconsistentNaming

        #region Nested type: WorkspaceAsanaClientCtr

        [TestFixture]
        public class WorkspaceAsanaClientCtr
        {
            [Test]
            public void create_worspace_asana_client()
            {
                var restClient = new TestRestClient("");
                var workspaceAsanaClient = new AsanaClient.WorkspaceAsanaClient(new AsanaClient(restClient));

                Assert.That(workspaceAsanaClient, Is.Not.Null);
            }
        }

        #endregion

        #region Nested type: WorkspaceAsanaClientGetWorkspaceTasks

        public class WorkspaceAsanaClientGetWorkspaceTasks
        {
            [Test]
            public void success_non_zero_tasks_short_request()
            {
                var workspaceAsanaClient =
                    AsanaClientTestHelper.GetAsanaClient<AsanaClient.WorkspaceAsanaClient>(short_workspace_response_content);

                const int randomWorkspaceId = 14619;

                var tasks = workspaceAsanaClient.GetWorkspaceTasks(randomWorkspaceId, UserPredefinedId.Me);

                Assert.That(tasks, Is.Not.Null);
                Assert.That(tasks.Count, Is.GreaterThan(0));
                Assert.That(tasks[0].Name, Is.EqualTo("test task"));
            }
        }

        #endregion
    }
}