namespace NAsana.API.v1
{
    using System.Collections.Generic;
    using Model;
    using Model.Utils;
    using RestSharp;
    using Utils;

    public partial class AsanaClient
    {
        public WorkspaceAsanaClient Workspace
        {
            get { return new WorkspaceAsanaClient(this); }
        }

        public class WorkspaceAsanaClient : AsanaClient<WorkspaceAsanaClient>
        {
            public WorkspaceAsanaClient(AsanaClient asanaClient)
                : base(asanaClient.Client)
            {
            }

            public List<Workspace> GetWorkspaces()
            {
                var request = AsanaRequest.Get("workspaces");
                return ExecuteRequest<List<Workspace>>(request);
            }

            public List<Task> GetWorkspaceTasks(long workspaceId, UserId assigneeId)
            {
                Guard.GreaterThan("workspaceId", workspaceId, 0);

                var request = AsanaRequest.Get(string.Format("tasks"));

                request.AddParameter(RequestParamNames.Assignee, assigneeId.ToString());
                request.AddParameter(RequestParamNames.Workspace, workspaceId);

                return ExecuteRequest<List<Task>>(request);
            }

            public List<Project> GetWorkspaceProjects(int workspaceId)
            {
                Guard.GreaterThan("workspaceId", workspaceId, 0);

                var request = AsanaRequest.Get(string.Format("workspaces/{0}/projects", workspaceId));
                return ExecuteRequest<List<Project>>(request);
            }

            public List<User> GetWorkspaceUsers(int workspaceId)
            {
                Guard.GreaterThan("workspaceId", workspaceId, 0);

                var request = AsanaRequest.Get(string.Format("workspaces/{0}/users", workspaceId));
                return ExecuteRequest<List<User>>(request);
            }

            public Workspace UpdateWorkspace(Workspace workspace)
            {
                var request = AsanaRequest.Put(string.Format("workspaces/{0}", workspace.Id),
                                               new Parameter
                                                   {
                                                       Name = "name",
                                                       Value = workspace.Name,
                                                   });
                return ExecuteRequest<Workspace>(request);
            }

            public Task CreateTask(int workspaceId, Task task)
            {
                Guard.GreaterThan("workspaceId", workspaceId, 0);
                Guard.NotNull("task", task);

                return CreateTaskImpl(task, workspaceId, null);
            }

        }
    }
}