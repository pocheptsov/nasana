namespace NAsana.API.v1.Model
{
    using System;
    using System.Collections.Generic;
    using Utils;

    public class Project : Identity, IStoryTarget
    {
        public bool Archived { get; set; }
        //created_at
        public DateTime CreatedAt { get; set; }
        public List<User> Followers { get; set; }
        //modified_at
        public DateTime ModifiedAt { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public Workspace Workspace { get; set; }
    }
}