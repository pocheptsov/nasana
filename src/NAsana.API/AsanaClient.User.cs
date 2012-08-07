namespace NAsana.API.v1
{
    using System.Collections.Generic;
    using Model;
    using Model.Utils;
    using Utils;

    public partial class AsanaClient
    {
        public UserAsanaClient User
        {
            get
            {
                return new UserAsanaClient(this);
            }
        }

        public class UserAsanaClient : AsanaClient
        {
            public UserAsanaClient(AsanaClient asanaClient)
                : base(asanaClient.Client)
            {
            }

            public User GetCurrentUser()
            {
                var request = AsanaRequest.Get("users/me");
                return ExecuteRequest<User>(request);
            }

            public User GetUser(UserId userId)
            {
                Guard.IsTrue("userId", () => userId.ToString().Length>0);

                var request = AsanaRequest.Get(string.Format("users/{0}", userId));
                return ExecuteRequest<User>(request);
            }

            public List<User> GetUsers()
            {
                var request = AsanaRequest.Get("users");
                return ExecuteRequest<List<User>>(request);
            }
        }
    }
}