namespace NAsana.API.v1.Model
{
    using System;
    using System.Collections.Generic;
    using Utils;

    public class Tag : Identity
    {
        public string Notes { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<User> Followers { get; set; }

        public Workspace Workspace { get; set; }
    }
}