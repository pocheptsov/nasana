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

        [JsonProperty(PropertyName = "assignee_status")]
        public AssigneeStatus AssigneeStatus { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "completed_at")]
        public DateTime? CompletedAt { get; set; }

        [JsonProperty(PropertyName = "due_on")]
        public DateTime? DueOn { get; set; }

        public List<User> Followers { get; set; }

        [JsonProperty(PropertyName = "modified_at")]
        public DateTime ModifiedAt { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public List<Project> Projects { get; set; }

        public Workspace Workspace { get; set; }
    }
}