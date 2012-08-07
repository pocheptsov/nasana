namespace NAsana.API.Tests
{
    using System.Linq;
    using Mocks;
    using NUnit.Framework;
    using PowerAssert;
    using v1;
    using v1.Model.Utils;

    public class WorkspaceAsanaClientTests : AsanaClientTests
    {
        private const string workspace_assigned_tasks_response_content = "{\"data\":[" +
                                                                "{\"id\":1384990514078,\"name\":\"Simple assigned task\"}," +
                                                                "{\"id\":1384990514076,\"name\":\"First assigned task\"}]}";

        [Test]
        public void create_worspace_asana_client()
        {
            var restClient = new TestRestClient("");
            var workspaceAsanaClient = new AsanaClient.WorkspaceAsanaClient(new AsanaClient(restClient));

            PAssert.IsTrue(() => workspaceAsanaClient != null);
        }

        [TestCase(workspace_assigned_tasks_response_content)]
        [TestCase(null)]
        public void success_non_zero_tasks_short_request(string responseContent)
        {
            var workspaceAsanaClient =
                GetAsanaClient<AsanaClient.WorkspaceAsanaClient>(responseContent);

            const long testWorkspaceId = 1384990514055;

            var tasks = workspaceAsanaClient.GetWorkspaceTasks(testWorkspaceId, UserPredefinedId.Me);

            PAssert.IsTrue(() => tasks != null);
            PAssert.IsTrue(() => tasks.Count > 0);

            var firstAssignedTaskInWorkspace = tasks.FirstOrDefault(_ => _.Name == "First assigned task");

            PAssert.IsTrue(() => firstAssignedTaskInWorkspace != null);
        }
    }
}