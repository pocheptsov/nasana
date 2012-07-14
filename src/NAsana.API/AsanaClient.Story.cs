namespace NAsana.API.v1
{
    using System.Collections.Generic;
    using Model;
    using Utils;

    public partial class AsanaClient
    {
        public StoryAsanaClient Story
        {
            get { return new StoryAsanaClient(this); }
        }

        public class StoryAsanaClient : AsanaClient
        {
            public StoryAsanaClient(AsanaClient asanaClient)
                : base(asanaClient.Client)
            {
            }

            public User GetCurrentUser()
            {
                var request = AsanaRequest.Get("users/me");
                return ExecuteRequest<User>(request);
            }

            public User GetUser(int userId)
            {
                Guard.GreaterThan("userId", userId, 0);

                var request = AsanaRequest.Get(string.Format("users/{0}", userId));
                return ExecuteRequest<User>(request);
            }

            public List<User> GetUsers(params UserFields[] optionalUserFields)
            {
                optionalUserFields = optionalUserFields ?? new[] {UserFields.Email, UserFields.Name};

                var request = AsanaRequest.Get("users");
                request.AddParameter(RequestParamNames.OptionalFields, string.Join(",", optionalUserFields));
                return ExecuteRequest<List<User>>(request);
            }
        }
    }
}