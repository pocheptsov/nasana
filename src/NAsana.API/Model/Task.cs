namespace NAsana.API.v1.Model
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Utils;

    [JsonObject(MemberSerialization.OptIn)]
    public class Task : Identity, IStoryTarget
    {
        public Task()
        {
            AssigneeStatus = AssigneeStatus.Upcoming;
        }

        /// <summary>
        /// Can be null
        /// </summary>
        public User Assignee { get; set; }

        public AssigneeStatus AssigneeStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime? DueOn { get; set; }

        public List<User> Followers { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public List<Project> Projects { get; set; }

        public Workspace Workspace { get; set; }
    }
}