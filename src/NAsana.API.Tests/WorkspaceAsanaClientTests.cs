namespace NAsana.API.Tests
{
    using Mocks;
    using NUnit.Framework;
    using v1;
    using v1.Model.Utils;

    public class WorkspaceAsanaClientTests
    {
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


        [TestFixture]
        public class WorkspaceAsanaClientGetWorkspaceTasks
        {
            [Test]
            public void success_non_zero_tasks_request()
            {
                var restClient = new TestRestClient("{\"data\":[{\"id\":1346674263886,\"name\":\"test task\"},{\"id\":1346674263887,\"name\":\"some not useful task\"},{\"id\":1346674263888,\"name\":\"moreover\"}]}");
                var workspaceAsanaClient = new AsanaClient.WorkspaceAsanaClient(new AsanaClient(restClient));

                var tasks = workspaceAsanaClient.GetWorkspaceTasks(14619, UserPredefinedId.Me);

                Assert.That(tasks, Is.Not.Null);
                Assert.That(tasks.Count, Is.GreaterThan(0));
                Assert.That(tasks[0].Name, Is.EqualTo("test task"));
            }
        }
    }
}