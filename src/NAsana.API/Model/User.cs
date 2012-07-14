namespace NAsana.API.v1.Model
{
    using System.Collections.Generic;
    using Utils;

    public class User : Identity
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public List<Workspace> Workspaces { get; set; }
    }
}