namespace NAsana.API.v1
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Model;
    using RestSharp;
    using Utils;

    public partial class AsanaClient
    {
        public TaskAsanaClient Task
        {
            get { return new TaskAsanaClient(this); }
        }

        public class TaskAsanaClient : AsanaClient
        {
            public TaskAsanaClient(AsanaClient asanaClient)
                : base(asanaClient.Client)
            {
            }

            public Task CreateTask(Task task, params string[] whitelist)
            {
                Guard.NotNull("task", task);
                Guard.NotNull("task.Workspace", task.Workspace);

                return CreateTaskImpl(task, task.Workspace.Id, whitelist);
            }

            public Task GetTask(int taskId)
            {
                Guard.GreaterThan("taskId", taskId, 0);

                var request = AsanaRequest.Get(string.Format("tasks/{0}", taskId));
                return ExecuteRequest<Task>(request);
            }

/*
            public List<User> UpdateTask(int taskId,)
            {
                optionalUserFields = optionalUserFields ?? new[] {UserFields.Email, UserFields.Name};

                var request = AsanaRequest.Put(string.Format("tasks/{0}", taskId));
                request.AddParameter(RequestParamNames.OptionalFields, string.Join(",", optionalUserFields));
                return ExecuteRequest<List<User>>(request);
            }
*/
        }
    }
}